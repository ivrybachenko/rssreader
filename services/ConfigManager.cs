using NLog;
using Newtonsoft.Json;
using Ninject;
using System.IO;
using System;

using RSSreader.Data;

namespace RSSreader
{
    namespace Services
    {
        public class ConfigManager : IConfigManager
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            string configPath;
            [Inject]
            public ConfigManager(IAppDirBuilder dirBuilder)
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Менеджер конфигураций. Инииализация начата...");
                }
                this.configPath = Path.Combine(dirBuilder.GetConfigDir(), "config.json");
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер конфигураций. Инициализация. "+
                        "Расположение конфига: {}.",this.configPath);
                }
                CreateIfNotExist();

                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Менеджер конфигураций. Инииализация завершена.");
                }
            }

            private void CreateIfNotExist()
            {
                if (!File.Exists(configPath))
                {
                    File.Create(configPath).Close();
                    UpdateConfig(new Config());
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("Менеджер конфигураций. Конфиг в указанном"+
                            "расположении отсутствовал. Создан новый.");
                    }
                }
            }
                
            public Config GetConfig()
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер конфигураций. Получен запрос на чтение конфигурации.");
                }
                CreateIfNotExist();
                string configdata = System.IO.File.ReadAllText(configPath);
                Config config = JsonConvert.DeserializeObject<Config>(configdata);
                return config;
            }

            public void UpdateConfig(Config newConfig)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер конфигураций. Получен запрос на обновление конфигурации.");
                }
                using (StreamWriter sw = new StreamWriter(configPath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    (new JsonSerializer()).Serialize(writer, newConfig);
                }
            }

            public void AddSubscription(string url)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер конфигураций. Получен запрос на добавление ленты {} в конфиг.",
                         url);
                }
                var config = GetConfig();
                config.AddSubscription(url);
                UpdateConfig(config);
            }

            public void RemoveSubscription(string url)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер конфигураций. Получен запрос на удаление ленты {} из конфига.",
                         url);
                }
                var config = GetConfig();
                config.RemoveSubscription(url);
                UpdateConfig(config);
            }

        }
        
    } /* namespace Services */
}