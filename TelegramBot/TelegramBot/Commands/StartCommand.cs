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
            + "\"/create_group название группы\" - Создать группу" + Environment.NewLine
            + "\"/show_groups\"- Посмотреть список групп" + Environment.NewLine
            + "\"/show_my_groups\"- Посмотреть список групп, в которых вы состоите" + Environment.NewLine
            + "\"/show_in_group название группы\"- Посмотреть список людей в группе" + Environment.NewLine
            + "\"/show_created_groups\"- Посмотреть список созданных вами групп" + Environment.NewLine
            + "\"/remove_group название группы\"- Удалить группу" + Environment.NewLine
            + "\"/add_to_group название группы\" - Присоединиться к группе" + Environment.NewLine
            + "\"/leave_group название группы\" - Выйти из группы" + Environment.NewLine
            + "\"/random_from_group название группы\" - Выбрать случайного человека из группы");
        }
    }
}
