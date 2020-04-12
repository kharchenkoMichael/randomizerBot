using System.Linq;
using System.Text;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class ShowCreatedGroupsCommand : Command
  {
    private readonly BotContext _botContext;

    public ShowCreatedGroupsCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/show_created_groups";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var builder = new StringBuilder();

      var userId = message.From.Id;

      builder.AppendLine("Вы создали такие группы:");

      foreach (var botContextGroup in _botContext.Groups.Where(item => item.CreatorId == userId))
      {
        var user = _botContext.Users.FirstOrDefault(item => item.Id == botContextGroup.CreatorId);
        builder.AppendLine($"{botContextGroup.Name} Создатель {user?.FirstName} @{user?.Name}");
      }

      client.SendTextMessageAsync(message.Chat.Id, builder.ToString());
    }
  }
}
