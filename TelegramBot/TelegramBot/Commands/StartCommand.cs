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
            _botContext.Users.Add(new User
            {
                ChatId = message.Chat.Id,
                Name = message.From.Username,
                FirstName = message.From.FirstName,
                Id = message.From.Id,
            });

            client.SendTextMessageAsync(message.Chat.Id, $"Привет");
        }
    }
}
