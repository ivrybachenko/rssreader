using System.ServiceModel.Syndication;

namespace RSSreader
{
    namespace Services
    {
        // Скачивает RSS-ленты из интернета
        public interface IRSSLoader
        {
            // Скачивает RSS-ленты из интернета
            // Если скачать не удалось, возвращает null.
            SyndicationFeed Download(string url);
        }

    } /* namespace Services */
}