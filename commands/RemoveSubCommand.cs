using NLog;
using System;

using RSSreader.Data;
using RSSreader.Services;

namespace RSSreader
{
    namespace Commands
    {
        public class RemoveSubCommand : ICommand
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            IConfigManager configManager;
            public RemoveSubCommand(IConfigManager configManager){
                this.configManager = configManager;
            }
            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                if (args.Length<1){
                    Console.WriteLine("Слишком мало параметров. Используйте help для справки.");
                    return;
                }
                Config config = configManager.GetConfig();
                try {
                    int num = Convert.ToInt32(args[0]);
                    config.Subscriptions.RemoveAt(num);
                }
                catch{
                    Console.WriteLine("Неверный индекс удаляемого канала.");
                }
                configManager.UpdateConfig(config);
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }

            public string Help()
            {
                return "Удаляет RSS-ленту с указанным индексом (0-based) из конфигурационного файла.";
            }
        }

    } /* namespace Commands */
}