using NLog;
using System;
using System.ServiceModel.Syndication;

using RSSreader.Services;

namespace RSSreader
{
    namespace Commands
    {
        public class ListCommand : ICommand
        {            
            private static Logger logger = LogManager.GetCurrentClassLogger();

            IConfigManager configManager;
            IRSSStorage storage;
            IFeedPrinter feedPrinter;
            public ListCommand(IConfigManager configManager, IRSSStorage storage, IFeedPrinter feedPrinter)
            {
                this.configManager = configManager;
                this.storage = storage;
                this.feedPrinter = feedPrinter;
            }
            public void Execute(params string[] args)
            {
                logger.Trace("Выполняется команда {}...", this.GetType().Name);
                var subscriptions = configManager.GetConfig().Subscriptions;
                int shown_cnt = 0;
                foreach (var url in subscriptions)
                {
                    var feed = storage.LoadFromStorage(url);
                    if (feed==null)
                    {
                        continue;
                    }
                    else
                    {
                        shown_cnt++;
                        feedPrinter.PrintFeed(feed);
                    }
                    
                }
                if (shown_cnt==0)
                {
                    Console.WriteLine("Нечего отображать.");
                    return;
                }
                logger.Trace("Выполнение команды {} завершено.", this.GetType().Name);
            }

            public string Help()
            {
                return "Читает скаченные локально RSS ленты и отображает их элементы.";
            }
        }

    } /* namespace Commands */
}