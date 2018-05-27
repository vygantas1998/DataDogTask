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
            List<Brewery> breweries = new List<Brewery>();
            List<GeoCode> geoCodes = new List<GeoCode>();
            List<Beer> beers = new List<Beer>();
            breweries = ReadData<Brewery>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/breweries.csv", "breweries.csv");
            geoCodes = ReadData<GeoCode>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/geocodes.csv", "geocodes.csv");
            beers = ReadData<Beer>.ReadDataFromWeb("https://raw.githubusercontent.com/brewdega/open-beer-database-dumps/master/dumps/beers.csv", "beers.csv");
            double lat = 51.355468;
            double longi = 11.100790;
            GeoCoordinate start = new GeoCoordinate(lat, longi);
            List<GeoCode> Nearest = geoCodes.FindAll(x => start.GetDistanceTo(x.Coordinates) <= 1000000);
            Trip(Nearest.FindAll(x => x.Accuracy == "ROOFTOP"), start, 2000000);
        }

        static void Trip(List<GeoCode> geoCodes, GeoCoordinate start, double startingDistance)
        {
            List<GeoCode> GeoCodes = geoCodes;
            GeoCoordinate pos = start;
            double distance = startingDistance;
            Console.WriteLine(start.Latitude + ", " + start.Longitude);
            while (pos.GetDistanceTo(start) <= distance)
            {
                GeoCode thisPos = GeoCodes.OrderByDescending(x => pos.GetDistanceTo(x.Coordinates)).Last();
                if (start.GetDistanceTo(thisPos.Coordinates) < distance)
                {
                    distance -= pos.GetDistanceTo(thisPos.Coordinates);
                    //Console.WriteLine(thisPos.BreweryId + " " + distance / 1000 + " " + pos.GetDistanceTo(thisPos.Coordinates)/1000);
                    Console.WriteLine(thisPos.Coordinates.Latitude + ", " + thisPos.Coordinates.Longitude);
                    pos = thisPos.Coordinates;
                    GeoCodes.Remove(thisPos);
                }
                else
                {
                    distance -= start.GetDistanceTo(thisPos.Coordinates);
                    pos = start;
                    Console.WriteLine(start.Latitude + ", " + start.Longitude);
                    break;
                }
            }
        }
    }
}
