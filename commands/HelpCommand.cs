using NLog;
using System;

using RSSreader.Services;

namespace RSSreader
{
    namespace Commands
    {
        public class HelpCommand : ICommand
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            ICommandEngine commandEngine;
            public HelpCommand(ICommandEngine commandEngine){
                this.commandEngine = commandEngine;
            }
            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                foreach (var item in commandEngine.GetRegisteredCommands()){
                    System.Console.WriteLine(item.Key);
                    System.Console.WriteLine("    "+item.Value.Help());
                }
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }
            public String Help(){
                return "Отображает список доступных команд.";
            }
        }
    } /* namespace Commands */
}