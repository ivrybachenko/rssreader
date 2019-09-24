using NLog;
using System;

using RSSreader.Services;

namespace RSSreader
{
    namespace Commands
    {
        public class ExitCommand : ICommand
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();
            IEnvironment environment;
            public ExitCommand(IEnvironment environment){
                this.environment = environment;
            }
            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                environment.Exit(0);
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }
            public String Help(){
                return "Завершает работу программы.";
            }
        }

    } /* namespace Commands */
}