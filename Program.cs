using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<Brewery> breweries = new List<Brewery>();
            List<GeoCode> geoCodes = new List<GeoCode>();
            List<Beer> beers = new List<Beer>();
            bool reDownload = false;
            if(args.Length == 3)
            {
                reDownload = bool.Parse(args[2]);
            }
            breweries = ReadData<Brewery>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/breweries.csv", "breweries.csv", reDownload);
            geoCodes = ReadData<GeoCode>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/geocodes.csv", "geocodes.csv", reDownload);
            beers = ReadData<Beer>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/beers.csv", "beers.csv", reDownload);
            double lat = double.Parse(args[0]);
            double longi = double.Parse(args[1]);
            GeoCoordinate start = new GeoCoordinate(lat, longi);
            List<GeoCode> trip = MakeTrip(geoCodes, start, 2000000);
            PrintData.PrintBreweries(trip, breweries);
            PrintData.PrintBeers(GetBeers(beers, trip));
            watch.Stop();
            PrintData.PrintProgramRuntime(watch);
        }

        static List<Beer> GetBeers(List<Beer> beers, List<GeoCode> geoCodes)
        {
            List<Beer> beersList = new List<Beer>();
            foreach (GeoCode geoCode in geoCodes)
            {
                foreach (Beer beer in beers.FindAll(x => x.BreweryId == geoCode.BreweryId))
                {
                    beersList.Add(beer);
                }
            }
            return beersList;
        }

        static List<GeoCode> MakeTrip(List<GeoCode> geoCodes, GeoCoordinate start, double startingDistance)
        {
            List<GeoCode> newList = new List<GeoCode>();
            List<GeoCode> GeoCodes = geoCodes.FindAll(x => x.Accuracy == "ROOFTOP");
            GeoCoordinate pos = start;
            double distance = startingDistance;

            newList.Add(new GeoCode(0, 0, start.Latitude, start.Longitude, "START"));

            while (pos.GetDistanceTo(start) <= distance)
            {
                GeoCode thisPos = GeoCodes.OrderByDescending(x => pos.GetDistanceTo(x.Coordinates)).Last();
                distance -= pos.GetDistanceTo(thisPos.Coordinates);
                if (thisPos.Coordinates.GetDistanceTo(start) > distance)
                {
                    break;
                }
                if (!newList.Exists(x => x.BreweryId == thisPos.BreweryId))
                {
                    pos = thisPos.Coordinates;
                    newList.Add(thisPos);
                }

                GeoCodes.Remove(thisPos);

            }
            newList.Add(new GeoCode(0, 0, start.Latitude, start.Longitude, "START"));
            return newList;
        }
    }
}
