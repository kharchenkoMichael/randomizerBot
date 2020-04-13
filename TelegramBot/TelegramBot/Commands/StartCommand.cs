using System;
using Telegram.Bot;
using TelegramBot.Models;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Commands
{
    public class StartCommand : Command
    {
        private readonly BotContext _botContext;

        public StartCommand(BotContext botContext)
        {
            _botContext = botContext;
        }

        public override string Name { get; } = "/start";
        public override void Execute(Message message, TelegramBotClient client)
        {
            client.SendTextMessageAsync(message.Chat.Id, $"Привет вы можете:" + Environment.NewLine
            + "\"/show\"- Посмотреть всех людей" + Environment.NewLine
            + "\"/add\" - Присоединиться" + Environment.NewLine
            + "\"/leave\" - Выйти" + Environment.NewLine
            + "\"/random\" - Выбрать случайного человека");
        }
    }
}
