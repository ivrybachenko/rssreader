using System.Collections.Generic;
using System;

using RSSreader.Commands;

namespace RSSreader
{
    namespace Services
    {
        // Управляет сопоставлением введённых пользователем команд и выполняемых действий
        public interface ICommandEngine
        {
            // Регистрирует команду
            // name - Строка для запуска команды
            // command - Выполняемая команда
            void RegisterCommand(String name, ICommand command);
            // Регистрирует действие, которое выполняется при неверном вводе названия команды
            void RegisterDefaultCommand(ICommand command);
            // Преобразует введённую пользователем строку в выполняемую команду
            ICommand GetCommand(String name);
            // Возвращает словарь всех зарегестрированных команд
            Dictionary<String, ICommand> GetRegisteredCommands();
        }

    } /* namespace Services */
}