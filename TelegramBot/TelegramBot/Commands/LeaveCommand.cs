using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class LeaveCommand : Command
  {
    private readonly BotContext _botContext;

    public LeaveCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/leave";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var userId = message.From.Id;

      var user = _botContext.Users.FirstOrDefault(item => item.Id == userId);
      if (user != null)
      {
        _botContext.Users.Remove(user);

        client.SendTextMessageAsync(message.Chat.Id, $"Вы успешно вышли");
      }
      else
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Вы уже вышли");
      }
    }
  }
}
