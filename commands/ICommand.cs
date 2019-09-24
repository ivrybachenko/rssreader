using System;

namespace RSSreader
{
    namespace Commands
    {
        // Команда командной строки
        public interface ICommand{
            // Выполняет действие связанное с командой
            void Execute(params string[] args);
            // Возвращает справку о команде
            String Help();
        }

    } /* namespace Commands */
}