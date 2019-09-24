using System.ServiceModel.Syndication;

namespace RSSreader
{
    namespace Services
    {
        // Отвечает за вывод ленты на консоль 
        public interface IFeedPrinter
        {
            // Выводит ленту на консоль
            void PrintFeed(SyndicationFeed feed);
        }
    }
}