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
    private readonly BotContext _botContext;
    private TelegramBotClient client;
    private Logger _logger;

    public List<Command> GetCommands()
    {
      return new List<Command>
      {
        new StartCommand(_botContext, _logger),
        new AddCommand(_botContext, _logger),
        new LeaveCommand(_botContext, _logger),
        new RandomCommand(_botContext, _logger),
        new ShowUsersCommand(_botContext, _logger),
      };
    }

    public Bot(AppSettings settings, BotContext botContext, Logger logger)
    {
      _settings = settings;
      _botContext = botContext;
      _logger = logger;
    }

    public async Task<TelegramBotClient> Get()
    {
      if (client != null)
        return client;

      client = new TelegramBotClient(_settings.Key);
      var hook = string.Format(_settings.Url, "api/message/update");
      await client.SetWebhookAsync(hook);

      return client;
    }
  }
}
