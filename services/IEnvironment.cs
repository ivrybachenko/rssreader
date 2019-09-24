namespace RSSreader
{
    namespace Services
    {
        // Обеспечивает доступ к системным функциям
        public interface IEnvironment
        {
            // Завершает работу приложения
            void Exit(int code);
            // Проверяет существует ли файл с указанным именем
            bool FileExists(string fileName);
        }
        
    } /* namespace Services */
}