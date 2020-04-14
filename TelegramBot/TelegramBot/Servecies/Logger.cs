using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace TelegramBot.Servecies
{
  public class Logger
  {
    private readonly IHostingEnvironment _appEnvironment;
    private readonly string _filePath = "Logs/log.txt";

    public Logger(IHostingEnvironment appEnvironment)
    {
      _appEnvironment = appEnvironment;
    }

    public void Log(string log)
    {
      var builder = new StringBuilder(GetLogs());

      builder.AppendLine(log);

      using (var writer = new StreamWriter(Path.Combine(_appEnvironment.ContentRootPath, _filePath)))
        writer.Write(builder.ToString());
    }

    public string GetLogs()
    {
      var path = Path.Combine(_appEnvironment.ContentRootPath, _filePath);

      if (!File.Exists(path))
        return string.Empty;

      using (var reader = new StreamReader(path))
        return reader.ReadToEnd();
    }
  }
}
