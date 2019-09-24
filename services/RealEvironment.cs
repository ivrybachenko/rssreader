using NLog;

namespace RSSreader
{
    namespace Services
    {
        // Обеспечивает доступ к системным функциям
        public class RealEnvironment : IEnvironment
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            public RealEnvironment()
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Среда выполнения. Инициализация начата...");
                }
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Среда выполнения. Инициализация завершена.");
                }
            }

            public void Exit(int code)
            {
                if (logger.IsTraceEnabled)
                {
                    logger.Trace("Среда выполнения. Поступил запрос на завершение работы приложения.");
                }
                
                System.Environment.Exit(code);
            }

            public bool FileExists(string fileName)
            {
                return System.IO.File.Exists(fileName);
            }
        }
        
    } /* namespace Services */
}