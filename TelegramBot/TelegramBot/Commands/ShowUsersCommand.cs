using System;
using System.Text;
using Telegram.Bot;
using TelegramBot.Models;
using TelegramBot.Servecies;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class ShowUsersCommand : Command
  {
    private readonly BotContext _botContext;
    private readonly Logger _logger;

    public ShowUsersCommand(BotContext botContext, Logger logger)
    {
      _botContext = botContext;
      _logger = logger;
    }

    public override string Name { get; } = "/show";

    public override void Execute(Message message, TelegramBotClient client)
    {
      _logger.Log($"{DateTime.Now} {message.From.Id} {message.From.Username} ShowUsersCommand");
      _botContext.UpdateFromJson();
      var builder = new StringBuilder();

      builder.AppendLine($"К нам присоединилось {_botContext.Users.Count} людей:");

      foreach (var user in _botContext.Users)
        builder.AppendLine($"{user.FirstName} @{user.Name}");

      client.SendTextMessageAsync(message.Chat.Id, builder.ToString());
    }
  }
}
