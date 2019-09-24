using NLog;
using Ninject;

using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace RSSreader
{
    namespace Services
    {
        public class RSSStorage : IRSSStorage
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            string storagePath;
            [Inject]
            public RSSStorage(IAppDirBuilder dirBuilder)
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Хранилище. Инициализация начата...");
                }
                this.storagePath = dirBuilder.GetStorageDir();
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Хранилище. Инициализация завершена");
                }
            }
            public void SaveToStorage(string url, SyndicationFeed channel)
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Хранилище. Получен запрос на сохранение ленты из {}.", url);
                }
                var channelToWrite = combineOldChannelWithNewOne(url, channel);
                string filePath = getFileName(url);
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("Хранилище. Для записи ленты {} создан новый файл.", url);
                    }
                }
                XmlWriter writer = XmlWriter.Create(filePath);
                channelToWrite.SaveAsRss20(writer);
                writer.Close();

                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Хранилище. Запрос на сохранение ленты из {} успешно обработан.", url);
                }
            }
            public SyndicationFeed LoadFromStorage(string url)
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Хранилище. Получен запрос на чтение ленты из {}.", url);
                }
                string filePath = getFileName(url);
                if (!File.Exists(filePath))
                {
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("Хранилище. Лента {} в хранилище не найдена.", url);
                    }
                    return null;
                }
                XmlReader FeedReader = XmlReader.Create(filePath);
                SyndicationFeed Channel = SyndicationFeed.Load(FeedReader);

                FeedReader.Close();
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Хранилище. Запрос на чтение ленты из {} усешно обработан.", url);
                }
                return Channel;
            }
            
            private string getFileName(string url){
                string fileName = url.Replace('/','_')+".xml";
                string filePath = Path.Combine(storagePath, fileName);
                return filePath;
            }
            private SyndicationFeed combineOldChannelWithNewOne(string url, SyndicationFeed newChannel){
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Хранилище. Слияние скачанной ленты {} с локальной...", url);
                }
                var newItems = newChannel.Items.ToList();
                var oldItems = new List<SyndicationItem>();
                SyndicationFeed oldChannel = LoadFromStorage(url);
                if (oldChannel!=null)
                {
                    oldItems = oldChannel.Items.ToList();            
                }
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Хранилище. Старая версия содержит {} элементов, новая - {}",
                     oldItems.Count, newItems.Count);
                }
                var combinedItems = oldItems;
                foreach (var item in newItems)
                {
                    if (oldItems.Find(s => s.Id==item.Id)==null)
                    {
                        if (logger.IsDebugEnabled)
                        {
                            logger.Debug("Хранилище. Слияние. Запись {} добавлена.", item.Id);
                        }
                        oldItems.Add(item);
                    }
                    else
                    {
                        if (logger.IsDebugEnabled)
                        {
                            logger.Debug("Хранилище. Слияние. Запись {} уже существует.", item.Id);
                        }
                    }
                }
                // Клонируем новую ленту, чтобы заголовок и описание было как в ней.
                var combinedChannel = newChannel.Clone(false);
                // Помещаем в ленту совмещённые записи.
                combinedChannel.Items = combinedItems;
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Хранилище. Слияние скачанной ленты {} с локальной заершено. "+
                        "Количество получившися элементов - {}.", url, combinedItems.Count);
                }
                return combinedChannel;
            }
        }
    } /* namespace Services */
}