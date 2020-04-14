using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using TelegramBot.Commands;
using TelegramBot.Models;
using TelegramBot.Servecies;
using Message = Telegram.Bot.Types.Message;
using User = TelegramBot.Models.User;

namespace TelegramBot.Controls
{
  public class MessageController : Controller
  {
    private readonly Bot _bot;
    private readonly BotContext _botContext;

    public MessageController(Bot bot, ILogger<MessageController> logger, BotContext botContext)
    {
      _bot = bot;
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
      var client = await _bot.Get();
      var commands = _bot.GetCommands();
      var userId = message.From.Id;

      var user = _botContext.Users.FirstOrDefault(item => item.Id == userId);
      
      commands.FirstOrDefault(command => command.Contains(message.Text))?.Execute(message, client);
    }
  }
}
