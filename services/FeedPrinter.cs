using System.ServiceModel.Syndication;
using System;

namespace RSSreader
{
    namespace Services
    {
        public class FeedPrinter : IFeedPrinter
        {
            public void PrintFeed(SyndicationFeed feed){
                Console.WriteLine(feed.Title.Text);
                Console.WriteLine("==============================");
                foreach (var item in feed.Items){
                    Console.WriteLine(item.Title.Text);
                    Console.WriteLine(item.PublishDate);
                    foreach(var link in item.Links){
                        Console.WriteLine(link.Uri);
                    }
                    Console.WriteLine("------------------------------");
                }
            }
        }
    }
}