using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Brewery> breweries = new List<Brewery>();
            List<GeoCode> geoCodes = new List<GeoCode>();
            List<Beer> beers = new List<Beer>();
            breweries = ReadData<Brewery>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/breweries.csv");
            geoCodes = ReadData<GeoCode>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/geocodes.csv");
            beers = ReadData<Beer>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/beers.csv");
        }
    }
}
