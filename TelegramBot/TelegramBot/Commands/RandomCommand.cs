using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class RandomCommand : Command
  {
    private readonly BotContext _botContext;

    public RandomCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/random";

    public override void Execute(Message message, TelegramBotClient client)
    {
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
