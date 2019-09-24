using NLog;
using NLog.Targets;
using NLog.Config;

using System.IO;

using RSSreader.Services;

namespace RSSreader
{

    class LogConfiguration
    {
        private string logFolderPath;
        private string logFilePath;
        private LogLevel logLevel = LogLevel.Info;
        public LogConfiguration(IAppDirBuilder dirBuilder, IConfigManager configManager)
        {
            var config = configManager.GetConfig();
            try
            {
                logLevel = LogLevel.FromString(config.LogLevel);
            }
            catch
            {
                config.LogLevel = logLevel.Name;
                configManager.UpdateConfig(config);
            }
            logFolderPath = dirBuilder.GetLogDir();
            logFilePath = Path.Combine(logFolderPath, "log.txt");
        }
        public void Configure()
        {
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets
             var consoleTarget = new ColoredConsoleTarget("target1")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("target2")
            {
                FileName = logFilePath,
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.AddTarget(fileTarget);

            // Step 3. Define rules
            config.AddRule(logLevel, LogLevel.Fatal, fileTarget);

            // Step 4. Activate the configuration
            LogManager.Configuration = config;

        }
    }

}