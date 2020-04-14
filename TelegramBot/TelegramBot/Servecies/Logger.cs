using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace TelegramBot.Servecies
{
  public class Logger
  {
    private readonly IHostingEnvironment _appEnvironment;
    private readonly string _filePath = "Logs/log-{0}.txt";

    public Logger(IHostingEnvironment appEnvironment)
    {
      _appEnvironment = appEnvironment;
    }

    public void Log(string log)
    {
      var builder = new StringBuilder(GetLogs(DateTime.Today));

      builder.AppendLine(log);

      var path = string.Format(_filePath, DateTime.Today.Day);

      File.Delete(Path.Combine(_appEnvironment.ContentRootPath, string.Format(_filePath, DateTime.Today.AddDays(-27).Day)));

      using (var writer = new StreamWriter(Path.Combine(_appEnvironment.ContentRootPath, path)))
        writer.Write(builder.ToString());
    }

    public string GetLogs(DateTime date)
    {
      var path = Path.Combine(_appEnvironment.ContentRootPath, string.Format(_filePath, date.Day));

      if (!File.Exists(path))
        return string.Empty;

      using (var reader = new StreamReader(path))
        return reader.ReadToEnd();
    }
  }
}
