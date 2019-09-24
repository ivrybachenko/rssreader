using NLog;
using System.Linq;

using RSSreader.Data;
using RSSreader.Services;

namespace RSSreader
{
    namespace Commands
    {
        public class PullCommand : ICommand
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            IConfigManager configManager;
            IRSSStorage storage;
            IRSSLoader loader;
            public PullCommand(IConfigManager configManager, IRSSStorage storage, IRSSLoader loader)
            {
                this.configManager = configManager;
                this.storage = storage;
                this.loader = loader;  
            }
            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                Config config = configManager.GetConfig();
                foreach (var url in config.Subscriptions){
                    System.Console.WriteLine("Загружаем из " + url);
                    var data = loader.Download(url);
                    if (data==null){
                        System.Console.WriteLine("    Не удалось загрузить канал.");
                    }
                    else{
                        System.Console.WriteLine("    Загружен канал: " + data.Title.Text);
                        System.Console.WriteLine("    Загружено записей: " + data.Items.Count());
                        storage.SaveToStorage(url, data);
                    }
                }
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }

            public string Help()
            {
                return "Читает RSS ленты из файла настроек, скачивает их и сохраняет на локальный диск.";
            }
        }

    } /* namespace Commands */
}