using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class AddToGroupCommand : Command
  {
    private readonly BotContext _botContext;

    public AddToGroupCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/add_to_group";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var name = message.Text.Replace(Name, "").ToLower();

      if (string.IsNullOrWhiteSpace(name))
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Нельзя добавить в группу без имени");
        return;
      }

      name = name.Trim();

      var group = _botContext.Groups.FirstOrDefault(item => item.Name == name);

      if (group == null)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"{name} не существует");
        return;
      }

      var userId = message.From.Id;
      if (_botContext.UserGroups.Any(item => item.Group == group.Name && item.UserId == userId))
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Вы уже состоите в группе {name}");
        return;
      }

      _botContext.UserGroups.Add(new UserGroup
      {
        Group = group.Name,
        UserId = userId
      });

      client.SendTextMessageAsync(message.Chat.Id, $"Вы успешно присоединились к группе {name}");
    }
  }
}
