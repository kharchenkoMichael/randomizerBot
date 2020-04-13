using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class AddCommand : Command
  {
    private readonly BotContext _botContext;

    public AddCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/add";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var userId = message.From.Id;

      if (_botContext.Users.All(item => item.Id != userId))
      {
        _botContext.Users.Add(new User
        {
          ChatId = message.Chat.Id,
          Name = message.From.Username,
          FirstName = message.From.FirstName,
          Id = message.From.Id,
        });

        client.SendTextMessageAsync(message.Chat.Id, $"Вы успешно присоединились");
      }
      else
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Вы уже присоединены");
      }
    }
  }
}
