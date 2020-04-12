using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using TelegramBot.Commands;
using TelegramBot.Models;
using TelegramBot.Servecies;
using Message = Telegram.Bot.Types.Message;

namespace TelegramBot.Controls
{
    public class MessageController : Controller
    {
        private readonly Bot _bot;
        private readonly ILogger<MessageController> _logger;
        private readonly BotContext _botContext;

        public MessageController(Bot bot, ILogger<MessageController> logger, BotContext botContext)
        {
            _bot = bot;
            _logger = logger;
            _botContext = botContext;
        }

        [HttpPost]
        [Route(@"api/message/update")] //webhook uri part
        public async Task<OkResult> Update([FromBody] Update update)
        {
            if (update.Message != null)
                await UpdateMessage(update.Message);
            
            //_botContext.WriteToJson();
            return Ok();
        }

        private async Task UpdateMessage(Message message)
        {
            _logger.LogInformation("{0}: {1} - {2}", message.From.FirstName, message.Text, DateTime.Now);
            var commands = _bot.GetCommands();
            var userId = message.From.Id;
            var client = await _bot.Get();

            var user = _botContext.Users.FirstOrDefault(item => item.Id == userId);

            if (user == null)
                new StartCommand(_botContext).Execute(message, client);
            else
            {
                user.Update(message);
                foreach (var command in commands)
                {
                    if (!command.Contains(message.Text))
                        continue;

                    command.Execute(message, client);
                    break;
                }
            }
        }
    }
}