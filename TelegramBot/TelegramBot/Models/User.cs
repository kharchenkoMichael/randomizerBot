using System.Linq;
using Microsoft.Extensions.Logging;

namespace TelegramBot.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public long ChatId { get; set; }
    
        public void Update(Telegram.Bot.Types.Message message)
        {
            Name = message.From.Username;
            FirstName = message.From.FirstName;
            ChatId = message.Chat.Id;
        }
    }
}
