using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace TelegramBot.Models
{
  public class BotContext
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly string _filePath = "Logs/logs-0.txt";

        public BotContext(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public List<User> Users { get; set; } = new List<User>();
        public List<Group> Groups { get; set; } = new List<Group>();

        public List<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        public User OwnerUser { get; set; }

        public Dictionary<int, int> MessageInDayDictionary = new Dictionary<int, int>();

        public void UpdateFromJson()
        {
            using (var reader = new StreamReader(Path.Combine(_appEnvironment.ContentRootPath, _filePath)))
            {
                BotContext m = JsonConvert.DeserializeObject<BotContext>(reader.ReadToEnd());
                Users = m.Users;
                Groups = m.Groups;
                UserGroups = m.UserGroups;
                OwnerUser = m.OwnerUser;
            }
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
