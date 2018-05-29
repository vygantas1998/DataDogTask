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
            double lat = 51.74250300;
            double longi = 19.43295600;
            GeoCoordinate start = new GeoCoordinate(lat, longi);
            List<GeoCode> trip = MakeTrip(geoCodes, start, 2000000);
            PrintBreweries(trip, breweries);
            foreach(GeoCode code in trip)
            {
                Console.WriteLine(code.ToString());
            }
        }

        static void PrintBreweries(List<GeoCode> trip, List<Brewery> breweries)
        {
            Console.WriteLine("Found {0} beer factories:", trip.Count - 2);
            int index = 0;
            double distance = 0;
            foreach (GeoCode geoCode in trip)
            {
                if (geoCode.Accuracy != "START")
                {
                    distance += geoCode.Coordinates.GetDistanceTo(trip[index - 1].Coordinates);
                    Console.WriteLine("       -> [{0}] {1}: {2}, {3} distance {4}km", geoCode.BreweryId, breweries.Find(x => x.Id == geoCode.BreweryId).Name, geoCode.Coordinates.Latitude, geoCode.Coordinates.Longitude, geoCode.Coordinates.GetDistanceTo(trip[index - 1].Coordinates) / 1000);
                }
                else if (index == trip.Count - 1)
                {
                    distance += geoCode.Coordinates.GetDistanceTo(trip[index - 1].Coordinates);
                    Console.WriteLine("       <- HOME: {0}, {1} distance {2}km", geoCode.Coordinates.Latitude, geoCode.Coordinates.Longitude, geoCode.Coordinates.GetDistanceTo(trip[index - 1].Coordinates) / 1000);
                }
                else
                {
                    Console.WriteLine("       -> HOME: {0}, {1} distance 0km", geoCode.Coordinates.Latitude, geoCode.Coordinates.Longitude);
                }
                index++;
            }
            Console.WriteLine("Total distance travelled: {0}", distance / 1000);
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
                pos = thisPos.Coordinates;
                GeoCodes.Remove(thisPos);
                newList.Add(thisPos);
            }
            newList.Add(new GeoCode(0, 0, start.Latitude, start.Longitude, "START"));
            return newList;
        }
    }
}
