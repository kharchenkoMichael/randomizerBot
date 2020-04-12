using System.Linq;
using System.Text;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class ShowGroupsCommand : Command
  {
    private readonly BotContext _botContext;

    public ShowGroupsCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/show_groups";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var builder = new StringBuilder();

      foreach (var botContextGroup in _botContext.Groups)
      {
        var user = _botContext.Users.FirstOrDefault(item => item.Id == botContextGroup.CreatorId);
        builder.AppendLine($"{botContextGroup.Name} Создатель {user?.FirstName} @{user?.Name}");
      }

      client.SendTextMessageAsync(message.Chat.Id, string.IsNullOrEmpty(builder.ToString())?"нет групп": builder.ToString());
    }
  }
}
