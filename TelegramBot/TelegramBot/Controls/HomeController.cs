using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TelegramBot.Models;
using TelegramBot.Servecies;

namespace TelegramBot.Controls
{
  public class HomeController : Controller
  {
    private IHostingEnvironment _appEnvironment;
    private readonly BotContext _botContext;
    private readonly Logger _logger;

    public HomeController(IHostingEnvironment environment, BotContext botContext, Logger logger)
    {
      _appEnvironment = environment;
      _botContext = botContext;
      _logger = logger;
    }

    public IActionResult Index()
    {
      _logger.Log("Home Index");
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

    public string BotContext()
    {
      return _botContext.GetJson();
    }

    public string Logs()
    {
      return _logger.GetLogs();
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