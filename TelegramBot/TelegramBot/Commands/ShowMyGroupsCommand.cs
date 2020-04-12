using System.Linq;
using System.Text;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class ShowMyGroupsCommand : Command
  {

    private readonly BotContext _botContext;

    public ShowMyGroupsCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/show_my_groups";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var builder = new StringBuilder();

      var userId = message.From.Id;

      builder.AppendLine("Вы состоите в таких группах:");

      foreach (var botContextGroup in _botContext.Groups.Where(item => _botContext.UserGroups.Any(ug => ug.UserId == userId && ug.Group == item.Name)))
      {
        var user = _botContext.Users.FirstOrDefault(item => item.Id == botContextGroup.CreatorId);
        builder.AppendLine($"{botContextGroup.Name} Создатель {user?.FirstName} @{user?.Name}");
      }

      client.SendTextMessageAsync(message.Chat.Id, builder.ToString());
    }
  }
}
