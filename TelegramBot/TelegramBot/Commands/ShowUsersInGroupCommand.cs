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

    public override string Name { get; } = "/show_users_in_group";

    public override void Execute(Message message, TelegramBotClient client)
    {

      var name = message.Text.Replace(Name, "").ToLower().Substring(1);
      var group = _botContext.Groups.FirstOrDefault(item => item.Name == name);

      if (group == null)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"{name} не существует");
        return;
      }

      var builder = new StringBuilder();
      builder.AppendLine("Вы состоите в таких группах:");

      foreach (var userGroup in _botContext.UserGroups.Where(item => item.Group == name))
        builder.AppendLine($"{_botContext.Users.FirstOrDefault(item => item.Id == userGroup.UserId)?.Name}");

      client.SendTextMessageAsync(message.Chat.Id, builder.ToString());
    }
  }
}
