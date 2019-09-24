using NLog;
using System.Collections.Generic;
using System;

using RSSreader.Commands;

namespace RSSreader
{
    namespace Services
    {
        public class CommandEngine : ICommandEngine
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            private ICommand defaultCommand;
            private Dictionary<String, ICommand> dict = new Dictionary<String, ICommand>();

            public CommandEngine()
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Командный процессор. Инициализация начата...");
                }
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Командный процессор. Инициализация завершена.");
                }
            }
            public void RegisterCommand(String name, ICommand command)
            {
                this.dict[name] = command;

                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Командный процессор. Зарегестрирована команда {} -> {}.",name, command);
                }
            }

            public void RegisterDefaultCommand(ICommand command)
            {
                this.defaultCommand = command;

                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Командный процессор. Зарегестрирована команда по умолчанию {}.",command);
                }
            }

            public ICommand GetCommand(String name)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Командный процессор. Запрошена команда {}", name);
                }

                ICommand command = defaultCommand;
                if (dict.ContainsKey(name))
                {
                    command = dict[name];
                }

                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Командный процессор. На запрос возвращена команда {}", command);
                }

                return command;
            }

            public Dictionary<string, ICommand> GetRegisteredCommands()
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Командный процессор. Запрошен список всех зарегестрированных команд.");
                }
                
                return dict;
            }
        }
    } /* namespace Services */
}