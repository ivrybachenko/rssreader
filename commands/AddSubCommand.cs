using NLog;

using System;

using RSSreader.Data;
using RSSreader.Services;

namespace RSSreader
{
    namespace Commands{

        public class AddSubCommand : ICommand
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();
            IConfigManager configManager;
            public AddSubCommand(IConfigManager configManager){
                this.configManager = configManager;
            }
            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                if (args.Length<1){
                    logger.Info("Команда отменена. Недостаточно параметров для выполнения команды.");
                    Console.WriteLine("Слишком мало параметров. Используйте help для справки.");
                    return;
                }
                configManager.AddSubscription(args[0]);
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }

            public string Help()
            {
                return "Добавляет RSS-ленту в конфигурационный файл.";
            }
        }

    } /* namespace Commands */
}