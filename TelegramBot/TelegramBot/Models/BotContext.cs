using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace TelegramBot.Models
{
  public class BotContext
  {
    private readonly IHostingEnvironment _appEnvironment;
    private readonly string _filePath = "Logs/bot.json";

    public BotContext(IHostingEnvironment appEnvironment)
    {
      _appEnvironment = appEnvironment;
    }

    public Random Random = new Random();

    public List<User> Users { get; set; } = new List<User>();

    public User OwnerUser { get; set; }

    public Dictionary<int, int> MessageInDayDictionary = new Dictionary<int, int>();

    public void UpdateFromJson()
    {
      var path = Path.Combine(_appEnvironment.ContentRootPath, _filePath);

      if (!File.Exists(path))
        return;

      using (var reader = new StreamReader(path))
      {
        BotContext m = JsonConvert.DeserializeObject<BotContext>(reader.ReadToEnd());
        Users = m.Users;
        OwnerUser = m.OwnerUser;
      }
    }

    public string GetJson()
    {
      var path = Path.Combine(_appEnvironment.ContentRootPath, _filePath);

      if (!File.Exists(path))
        return string.Empty;

      using (var reader = new StreamReader(path))
        return reader.ReadToEnd();
    }

    public void WriteToJson()
    {
      using (var writer = new StreamWriter(Path.Combine(_appEnvironment.ContentRootPath, _filePath)))
      {
        writer.Write(JsonConvert.SerializeObject(this));
      }
    }
  }
}
