using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class RemoveGroupCommand : Command
  {
    private readonly BotContext _botContext;

    public RemoveGroupCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/remove_group";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var name = message.Text.Replace(Name, "").ToLower();

      if (string.IsNullOrWhiteSpace(name))
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Нельзя удалить группу без имени");
        return;
      }

      name = name.Trim();

      var group = _botContext.Groups.FirstOrDefault(item => item.Name == name);
      if (group == null)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"{name} не существует");
        return;
      }

      if (group.CreatorId != message.From.Id)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Вы не создатель группы {name}");
        return;
      }

      _botContext.Groups.Remove(group);

      client.SendTextMessageAsync(message.Chat.Id, $"{name} успешно удалена");
    }
  }
}
