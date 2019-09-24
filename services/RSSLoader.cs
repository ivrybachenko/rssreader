using NLog;

using System.Xml;
using System.Linq;
using System.ServiceModel.Syndication;

namespace RSSreader
{
    namespace Services
    {
        class RSSLoader : IRSSLoader
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            public RSSLoader()
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Загрузчик лент. Инициализация начата...");
                }
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Загрузчик лент. Инициализация завершена.");
                }
            }
            public SyndicationFeed Download(string url)
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Загрузчик лент. Получен запрос на скачивние из {}.",url);
                }

                try
                {
                    
                // Создаем XmlReader дял чтения RSS/Atom
                XmlReader FeedReader = XmlReader.Create(url);

                // Загружаем RSS/Atom
                SyndicationFeed Channel = SyndicationFeed.Load(FeedReader);
                FeedReader.Close();
                
                logger.Info("Загрузчик лент. Лента {} загружена. Количесво записей: {}.",
                    url,Channel.Items.Count());
                
                return Channel;            
                
                }
                catch (System.Exception e)
                {
                    logger.Info("Загрузчик лент. Не удалось загрузить ленту {}. Ошибка: {}.",
                        url,e.Message);
                
                    return null;
                }

            }
        }
    } /* namespace Services */
}