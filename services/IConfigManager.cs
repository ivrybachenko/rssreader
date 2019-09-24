using RSSreader.Data;

namespace RSSreader
{
    namespace Services
    {
        // Управляет чтением и записью конфигурации программы
        public interface IConfigManager
        {
            // Читает файл конфигурации
            Config GetConfig();
            // Добавляет подписку в файл конфигурации
            void AddSubscription(string url);
            // Удаляет подписку из файла конфигурации
            void RemoveSubscription(string url);
            // Перезаписывает файл конфигурации
            void UpdateConfig(Config newConfig);
        }
        
    } /* namespace Services */
}