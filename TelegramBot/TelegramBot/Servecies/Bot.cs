using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using TelegramBot.Commands;
using TelegramBot.Models;

namespace TelegramBot.Servecies
{
  public class Bot
  {
    private readonly AppSettings _settings;
    private readonly ILogger<Bot> _logger;
    private readonly BotContext _botContext;
    private TelegramBotClient client;

    public List<Command> GetCommands()
    {
      return new List<Command>
      {
        new StartCommand(_botContext),
        new AddCommand(_botContext),
        new LeaveCommand(_botContext),
        new RandomCommand(_botContext),
        new ShowUsersCommand(_botContext),
      };
    }

    public Bot(AppSettings settings, ILogger<Bot> logger, BotContext botContext)
    {
      _settings = settings;
      _logger = logger;
      _botContext = botContext;
    }

    public async Task<TelegramBotClient> Get()
    {
      if (client != null)
      {
        return client;
      }

      client = new TelegramBotClient(_settings.Key);
      _logger.LogInformation("Get Bot");
      var hook = string.Format(_settings.Url, "api/message/update");
      await client.SetWebhookAsync(hook);

      return client;
    }
  }
}
