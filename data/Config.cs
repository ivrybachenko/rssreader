using NLog;
using Newtonsoft.Json;

using System;
using System.Linq;
using System.Collections.Generic;

namespace RSSreader
{
    namespace Data
    {
        public class Config
        {
            private static Logger logger = LogManager.GetCurrentClassLogger();

            [JsonProperty("logLevel")]
            public string LogLevel = "Info";
            [JsonProperty("subscriptions")]
            public List<string> Subscriptions{get;set;} = new List<string>();
            // Добавляет подписку в конфиг
            public void AddSubscription(string url)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Конфиг. Получен запрос на добавление ленты {}.", url);
                }
                bool presented = (FindByURL(url) >= 0);
                if (!presented)
                {
                    Subscriptions.Add(url);
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("Конфиг. Лента {} добавлена в конфиг.", url);
                    }
                }
                else
                {
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("Конфиг. Лента {} уже присутствует в конфиге.", url);
                    }
                }
            }
            // Удаляет подписку из конфига
            public void RemoveSubscription(string url)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Конфиг. Получен запрос на удаление ленты {}.", url);
                }
                int index = FindByURL(url);
                if (index < 0)
                {
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("Конфиг. Лента {} не удалена, т.к. не найдена.", url);
                    }
                    return;
                }
                Subscriptions.RemoveAt(index);
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Конфиг. Лента {} удалена из конфига.", url);
                }
            }
            // Возвращает индекс url в Subscriptions. Если не найдено, то -1. 
            private int FindByURL(string url)
            {
                for (int i = 0; i<Subscriptions.Count; i++)
                {
                    var curUrl = Subscriptions[i];
                    if (curUrl==url)
                    {
                        if (logger.IsDebugEnabled)
                        {
                            logger.Debug("Конфиг. Лента {} имеет индекс {}.", url, i);
                        }
                        return i;
                    }
                }
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Конфиг. Лента {} отсутствует в конфиге.", url);
                }
                return -1;
            }
            public bool EqualsByValue(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }
                var cfg = obj as Config;
                return Equals(cfg.LogLevel, LogLevel) &&
                    Enumerable.SequenceEqual(cfg.Subscriptions, Subscriptions);
            }

            public override int GetHashCode()
            {
                var hashCode = -168896346;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LogLevel);
                hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(Subscriptions);
                return hashCode;
            }
        }
    }

}