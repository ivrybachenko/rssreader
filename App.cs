using Ninject;
using NLog;
using System.Linq;
using System;

using RSSreader.Commands;
using RSSreader.Services;

namespace RSSreader
{

    class App
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ICommandEngine commandEngine;
        public App()
        {
            KernelBase kernel = new StandardKernel(new Bindings());
            kernel.Get<LogConfiguration>().Configure();
            InitCommandEngine(kernel);
        }
        private void InitCommandEngine(KernelBase kernel)
        {
            if (logger.IsTraceEnabled)
            {
                logger.Trace("Настройка командного процессора...");
            }
            commandEngine = kernel.Get<ICommandEngine>();

            commandEngine.RegisterDefaultCommand(kernel.Get<WrongInputCommand>());
            commandEngine.RegisterCommand("help", kernel.Get<HelpCommand>());
            commandEngine.RegisterCommand("exit", kernel.Get<ExitCommand>());
            commandEngine.RegisterCommand("pull", kernel.Get<PullCommand>());
            commandEngine.RegisterCommand("list", kernel.Get<ListCommand>());
            commandEngine.RegisterCommand("remove", kernel.Get<RemoveCommand>());
            commandEngine.RegisterCommand("subs", kernel.Get<SubsCommand>());
            commandEngine.RegisterCommand("addsub", kernel.Get<AddSubCommand>());
            commandEngine.RegisterCommand("remsub", kernel.Get<RemoveSubCommand>());
            commandEngine.RegisterCommand("path", kernel.Get<AppPathCommand>());
            
            if (logger.IsTraceEnabled)
            {
                logger.Trace("Настройка командного процессора завершена.");
            }
        }

        public void Run()
        {
            logger.Info("Программа готова к работе.");
            Console.WriteLine("Программа для чтения RSS-лент.");
            while(true)
            {
                Console.Write("> ");
                String[] input = Console.ReadLine().Split();
                String cmd_name = input[0];
                String[] cmd_args = input.TakeLast(input.Length-1).ToArray();
                logger.Info("Получена команда {}. Аргументы: {}.", cmd_name, cmd_args);
                ICommand command = commandEngine.GetCommand(cmd_name);
                try
                {
                    command.Execute(cmd_args);
                }
                catch (Exception e)
                {
                    Console.WriteLine("При выполнении команды произошла ошибка. "+
                     "Обратитесь к логу, чтобы узнать подробности.");
                    Console.WriteLine("Ошибка: {0}",e.Message);
                    logger.Error("При выполнении команды произошла ошибка.\n{}\n{}",e.Message,e.StackTrace);
                }
            }
        }
    }

}