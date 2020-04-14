using System;
using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using TelegramBot.Servecies;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class LeaveCommand : Command
  {
    private readonly BotContext _botContext;
    private readonly Logger _logger;

    public LeaveCommand(BotContext botContext, Logger logger)
    {
      _botContext = botContext;
      _logger = logger;
    }

    public override string Name { get; } = "/leave";

    public override void Execute(Message message, TelegramBotClient client)
    {
      _logger.Log($"{DateTime.Now} {message.From.Id} {message.From.Username} LeaveCommand");
      _botContext.UpdateFromJson();
      var userId = message.From.Id;

      var user = _botContext.Users.FirstOrDefault(item => item.Id == userId);
      if (user != null)
      {
        _botContext.Users.Remove(user);

        _botContext.WriteToJson();
        client.SendTextMessageAsync(message.Chat.Id, $"Вы успешно вышли");
      }
      else
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Вы уже вышли");
      }
    }
  }
}
