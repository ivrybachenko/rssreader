using NLog;

using System.IO;
using System;

namespace RSSreader
{
    namespace Services
    {
        public class MyDocumentsAppDirBuilder : IAppDirBuilder
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            string appFolder;
            string storageFolder;
            string configFolder;
            string logFolder;
            public MyDocumentsAppDirBuilder()
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Менеджер диреторий. Инициализация начата...");
                }
                appFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "_rssreader");
                Directory.CreateDirectory(appFolder);
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер диреторий. Создана корневая папка: {}", appFolder);
                }

                storageFolder = Path.Combine(appFolder, "data");
                Directory.CreateDirectory(storageFolder);
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер диреторий. Создана папка хранилища: {}", storageFolder);
                }

                configFolder = Path.Combine(appFolder,"config");
                Directory.CreateDirectory(configFolder);
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер диреторий. Создана папка конфигов: {}", configFolder);
                }

                logFolder = Path.Combine(appFolder,"log");
                Directory.CreateDirectory(logFolder);
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Менеджер диреторий. Создана папка логов: {}", logFolder);
                }

                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Менеджер диреторий. Инициализация завершена.");
                }
            }
            public string GetConfigDir()
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Менеджер диреторий. Запрошен путь к папке с конфигами.");
                }
                return configFolder;
            }

            public string GetRootDir()
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Менеджер диреторий. Запрошен путь к корневой папке.");
                }
                return appFolder;
            }

            public string GetStorageDir()
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Менеджер диреторий. Запрошен путь к папке хранилища.");
                }
                return storageFolder;
            }

            public string GetLogDir()
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Менеджер диреторий. Запрошен путь к папке логов.");
                }
                return logFolder;
            }
        }
    } /* namespace Services */
}