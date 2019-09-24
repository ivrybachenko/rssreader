using System;

namespace RSSreader
{
    namespace Services
    {
        // Создаёт папки, необходимые для хранения данных программы
        public interface IAppDirBuilder
        {
            // Возвращает путь до корневой папки с данными приложения
            string GetRootDir();
            // Возвращает путь до папки с конфигами
            string GetConfigDir();
            // Возвращает путь до папки с RSS-лентами
            string GetStorageDir();
            // Возвращает путь до папки с логами
            string GetLogDir();
        } 

    } /* namespace Services */
}