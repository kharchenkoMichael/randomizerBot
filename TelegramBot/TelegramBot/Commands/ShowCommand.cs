using System.Text;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class ShowCommand : Command
  {
    private readonly BotContext _botContext;

    public ShowCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/show";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var builder = new StringBuilder();

      builder.AppendLine($"К нам присоединилось {_botContext.Users.Count} людей:");

      foreach (var user in _botContext.Users)
        builder.AppendLine($"{user.FirstName} @{user.Name}");

      client.SendTextMessageAsync(message.Chat.Id, builder.ToString());
    }
  }
}
