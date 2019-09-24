using NLog;
using System;

using RSSreader.Services;

namespace RSSreader
{
    namespace Commands
    {
        public class AppPathCommand : ICommand
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();
            IAppDirBuilder dirBuilder;
            public AppPathCommand(IAppDirBuilder dirBuilder){
                this.dirBuilder = dirBuilder;
            }
            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                Console.WriteLine(dirBuilder.GetRootDir());
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }

            public string Help()
            {
                return "Выводит путь путь до корневой папки с данными приложения.";
            }
        }
    }
}