using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;

namespace RSSreader
{
    namespace Services
    {
        // Управляет локальным хранилищем RSS-лент
        public interface IRSSStorage
        {
            // Сохраняет RSS-ленту в локальное хранилище.
            // Если лента, скачанная с этого url, уже имеется, то она дополняется новыми данными. 
            void SaveToStorage(string url, SyndicationFeed channel);
            // Читает RSS-ленту из локального хранилища
            // Если лента отсутствует, возращает null.
            SyndicationFeed LoadFromStorage(string url);
        }
        
    } /* namespace Services */
}