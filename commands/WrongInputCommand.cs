using NLog;
using System;

namespace RSSreader
{
    namespace Commands
    {
        public class WrongInputCommand : ICommand
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                Console.WriteLine("Неверная команда. Используйте help для справки.");
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }

            public string Help()
            {
                return "Выводит сообщение при неверном вводе команды";
            }
        }

    } /* namespace Commands */
}