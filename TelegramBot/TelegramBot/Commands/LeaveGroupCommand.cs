using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class LeaveGroupCommand : Command
  {
    private readonly BotContext _botContext;

    public LeaveGroupCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/leave_group";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var name = message.Text.Replace(Name, "").ToLower().Substring(1);
      var group = _botContext.Groups.FirstOrDefault(item => item.Name == name);

      if (group == null)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"{name} не существует");
        return;
      }

      var userId = message.From.Id;
      var ug = _botContext.UserGroups.FirstOrDefault(item => item.Group == group.Name && item.UserId == userId);
      if (ug == null)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Вы не состоите в группе {name}");
        return;
      }

      _botContext.UserGroups.Remove(ug);

      client.SendTextMessageAsync(message.Chat.Id, $"Вы успешно вышли из группы {name}");
    }
  }
}
