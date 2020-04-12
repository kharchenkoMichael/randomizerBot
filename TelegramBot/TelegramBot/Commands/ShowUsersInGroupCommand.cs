using System.Linq;
using System.Text;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class ShowUsersInGroupCommand : Command
  {
    private readonly BotContext _botContext;

    public ShowUsersInGroupCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/show_in_group";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var name = message.Text.Replace(Name, "").ToLower();

      if (string.IsNullOrWhiteSpace(name))
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Нельзя посмотреть участников группы без имени");
        return;
      }

      name = name.Trim();

      var group = _botContext.Groups.FirstOrDefault(item => item.Name == name);

      if (group == null)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"{name} не существует");
        return;
      }

      var userGroups = _botContext.UserGroups.Where(item => item.Group == name).ToList();

      var builder = new StringBuilder();
      builder.AppendLine($"В группе {name} состоят {userGroups.Count} человек:");

      foreach (var user in userGroups.Select(userGroup => _botContext.Users.FirstOrDefault(item => item.Id == userGroup.UserId)))
        builder.AppendLine($"{user?.FirstName} @{user?.Name}");

      client.SendTextMessageAsync(message.Chat.Id, builder.ToString());
    }
  }
}
