using System;
using Telegram.Bot;
using TelegramBot.Models;
using TelegramBot.Servecies;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class RandomCommand : Command
  {
    private readonly BotContext _botContext;
    private readonly Logger _logger;

    public RandomCommand(BotContext botContext, Logger logger)
    {
      _botContext = botContext;
      _logger = logger;
    }

    public override string Name { get; } = "/random";

    public override void Execute(Message message, TelegramBotClient client)
    {
      _logger.Log($"{DateTime.Now} {message.From.Id} {message.From.Username} RandomCommand");
      _botContext.UpdateFromJson();
      if (_botContext.Users.Count == 0)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Нет никого :(");
        return;
      }

      var users = _botContext.Users;

      var user = users[_botContext.Random.Next(users.Count)];

      client.SendTextMessageAsync(message.Chat.Id, $"Рандом выбрал {user.FirstName} @{user.Name}");
    }
  }
}
