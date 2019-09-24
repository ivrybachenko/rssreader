using NLog;
using System.IO;

using RSSreader.Services;

namespace RSSreader
{
    namespace Commands
    {
        public class RemoveCommand : ICommand
        {            
            private static Logger logger = LogManager.GetCurrentClassLogger();

            IAppDirBuilder dirBuilder;

            public RemoveCommand(IAppDirBuilder dirBuilder){
                this.dirBuilder = dirBuilder;
            }
            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                string storagePath = dirBuilder.GetStorageDir();
                var storageDirInfo = new DirectoryInfo(storagePath);
                foreach(var file in storageDirInfo.GetFiles()){
                    file.Delete();
                }
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }

            public string Help()
            {
                return "Удаляет все скаченные RSS ленты.";
            }
        }

    } /* namespace Commands */
}