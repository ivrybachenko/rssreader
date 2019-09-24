using NLog;
using System;

using RSSreader.Data;
using RSSreader.Services;

namespace RSSreader
{
    namespace Commands
    {
        public class SubsCommand : ICommand
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            IConfigManager configManager;
            public SubsCommand(IConfigManager cm){
                configManager = cm;
            }
            public void Execute(params string[] args)
            {    
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                Config s = configManager.GetConfig();
                Console.WriteLine("Подписки: ");
                foreach(string url in s.Subscriptions){
                    Console.WriteLine("    " + url);
                }
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }

            public string Help()
            {
                return "Выводит список RSS-лент, указанных в файле настроек.";
            }
        }

    } /* namespace Commands */
}