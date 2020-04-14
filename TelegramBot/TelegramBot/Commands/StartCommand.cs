using System;
using Telegram.Bot;
using TelegramBot.Models;
using TelegramBot.Servecies;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
  public class StartCommand : Command
  {
    private readonly BotContext _botContext;
    private readonly Logger _logger;

    public StartCommand(BotContext botContext, Logger logger)
    {
      _botContext = botContext;
      _logger = logger;
    }

    public override string Name { get; } = "/start";

    public override void Execute(Message message, TelegramBotClient client)
    {
      _logger.Log($"{DateTime.Now} {message.From.Id} {message.From.Username} StartCommand");

      client.SendTextMessageAsync(message.Chat.Id, $"Привет вы можете:" + Environment.NewLine
                                                                        + "\"/show\"- Посмотреть всех людей" +
                                                                        Environment.NewLine
                                                                        + "\"/add\" - Присоединиться" +
                                                                        Environment.NewLine
                                                                        + "\"/leave\" - Выйти" + Environment.NewLine
                                                                        + "\"/random\" - Выбрать случайного человека");
    }
  }
}
