using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MusicFadingOut
{
    class Config
    {
        public float fadeDurationSeconds = 15.0f;

        private static string configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "config.json");

        public static Config Instance { get; private set; } = new Config();

        private Config()
        {
        }

        public static void Load()
        {
            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath, JsonConvert.SerializeObject(Instance, Formatting.Indented));
                return;
            }

            var json = File.ReadAllText(configPath);
            var userSettings = JsonConvert.DeserializeObject<Config>(json);

            var fields = typeof(Config).GetFields();

            foreach (var field in fields)
            {
                var userValue = field.GetValue(userSettings);
                field.SetValue(Instance, userValue);
            }
        }
    }
}
