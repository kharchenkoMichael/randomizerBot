using System;
using System.Linq;
using Telegram.Bot;
using TelegramBot.Models;
using TelegramBot.Servecies;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class AddCommand : Command
  {
    private readonly BotContext _botContext;
    private readonly Logger _logger;


    public AddCommand(BotContext botContext, Logger logger)
    {
      _botContext = botContext;
      _logger = logger;
    }

    public override string Name { get; } = "/add";

    public override void Execute(Message message, TelegramBotClient client)
    {
      _logger.Log($"{DateTime.Now} {message.From.Id} {message.From.Username} AddCommand");
      _botContext.UpdateFromJson();
      var userId = message.From.Id;

      if (_botContext.Users.All(item => item.Id != userId))
      {
        _botContext.Users.Add(new User
        {
          ChatId = message.Chat.Id,
          Name = message.From.Username,
          FirstName = message.From.FirstName,
          Id = message.From.Id,
        });

        _botContext.WriteToJson();
        client.SendTextMessageAsync(message.Chat.Id, $"Вы успешно присоединились");
      }
      else
      {
        client.SendTextMessageAsync(message.Chat.Id, $"Вы уже присоединены");
      }
    }
  }
}
