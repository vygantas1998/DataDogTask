using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataDog
{
    class Program
    {
        List<Breweries> Breweries = new List<Breweries>();
        static void Main(string[] args)
        {
            ReadBreweries("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/breweries.csv");
        }
        public static List<Breweries> ReadBreweries(string location)
        {
            WebClient client = new WebClient();
            List<Breweries> breweries = new List<Breweries>();
            string file = client.DownloadString(location);
            string[] lines = file.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                line.Replace('\"', '"');
                Console.WriteLine(line);
                string[] data = line.Split(',');
            }
            //using(StreamReader reader = new StreamReader(@location))
            //{
            //    string line = "";
            //    while((line = reader.ReadLine()) != null)
            //    {
            //        string[] data = line.Split(',');
            //    }
            //}
            return breweries;
        }
    }
}
