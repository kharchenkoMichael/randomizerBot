using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class RandomFromGroupCommand : Command
  {
    private readonly BotContext _botContext;

    public RandomFromGroupCommand(BotContext botContext)
    {
      _botContext = botContext;
    }

    public override string Name { get; } = "/random_from_group";

    public override void Execute(Message message, TelegramBotClient client)
    {
      var name = message.Text.Replace(Name, "").ToLower().Substring(1);
      var group = _botContext.Groups.FirstOrDefault(item => item.Name == name);

      if (group == null)
      {
        client.SendTextMessageAsync(message.Chat.Id, $"{name} не существует");
        return;
      }

      var users = _botContext.UserGroups.Where(item => item.Group == group.Name).Select(item => item.UserId).ToList();

     var user = _botContext.Users.FirstOrDefault(item => item.Id == users[_botContext.Random.Next(users.Count)]);

      client.SendTextMessageAsync(message.Chat.Id, $"Рандом выбрал {user.Name} {user.FirstName}");
    }
  }
}
