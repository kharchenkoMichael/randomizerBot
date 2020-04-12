using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class CreateGroupCommand : Command
  {
    private readonly BotContext _botContext;

    public CreateGroupCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/create_group";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var name = message.Text.Replace(Name, "").ToLower();

      if (string.IsNullOrWhiteSpace(name))
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Нельзя создать группу без имени");
        return;
      }

      name = name.Trim();

      if (_botContext.Groups.Any(item => item.Name == name))
      {
        client.SendTextMessageAsync(message.Chat.Id, $"{name} уже существует");
        return;
      }

      _botContext.Groups.Add(new Group()
      {
        Name = name,
        CreatorId = message.From.Id,
      });

      client.SendTextMessageAsync(message.Chat.Id, $"{name} успешно создана");
    }
  }
}
