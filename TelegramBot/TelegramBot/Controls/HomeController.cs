using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TelegramBot.Models;

namespace TelegramBot.Controls
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private IHostingEnvironment _appEnvironment;
    private readonly BotContext _botContext;

    public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment, BotContext botContext)
    {
      _appEnvironment = environment;
      _botContext = botContext;
      _logger = logger;
    }

    public IActionResult Index()
    {
      //_botContext.UpdateFromJson();
      _logger.LogInformation("Home Controller");
      return View();
    }

    public IActionResult LogFile(int id)
    {
      // Путь к файлу
      string file_path = Path.Combine(_appEnvironment.ContentRootPath, $"Logs/logs-{id}.txt");
      // Тип файла - content-type
      string file_type = "text/plain";
      // Имя файла - необязательно
      string file_name = $"logs-{id}.txt";
      return PhysicalFile(file_path, file_type, file_name);
    }

    public string Log(int id)
    {
      // Путь к файлу
      string file_path = Path.Combine(_appEnvironment.ContentRootPath, $"Logs/logs-{id}.txt");
      using (StreamReader reader = new StreamReader(file_path))
      {
        return reader.ReadToEnd();
      }
    }

    public string LogDelete(int id)
    {
      // Путь к файлу
      string file_path = Path.Combine(_appEnvironment.ContentRootPath, $"Logs/logs-{id}.txt");
      System.IO.File.Delete(file_path);
      return $"успешно очищен {file_path}";
    }

    public IActionResult User()
    {
      return View(_botContext.Users);
    }
    
    public string Settings()
    {
      var builder = new StringBuilder();
      foreach (var s in _botContext.MessageInDayDictionary.Select(item =>
        $"баланс > {item.Key}, тогда {item.Value} сообщений в день"))
        builder.AppendLine(s);
      return builder.ToString();
    }
  }
}