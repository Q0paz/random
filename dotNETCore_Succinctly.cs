using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

using WidgetLibrary;

namespace Succinct_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () => { await MainAsync(args); }).Wait();
        }
        static async Task MainAsync(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                var blogURL = "https://blogs.msdn.microsoft.com/italy/feed";

                var xml = await client.GetStringAsync(blogURL);

                var reader = XDocument.Parse(xml);

                foreach (var item in reader.Descendants("item").Take(5))
                {
                    var title = item.Descendants("title").FirstOrDefault().Value;
                    Func<XElement, bool> p = x => x.Name.LocalName.Contains("creator");
                    var author = item.Descendants().FirstOrDefault(p).Value;

                    Console.WriteLine($"{title} \n{author}\n");
                }
            }
            Console.ReadLine();
        }
    }
}
