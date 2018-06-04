using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    class PrintData
    {
        public static void PrintBeers(List<Beer> Beers)
        {
            Console.WriteLine("Collected {0} beer types:", Beers.Count);
            foreach (Beer beer in Beers)
            {
                Console.WriteLine("       -> {0}", beer.Name);
            }
        }

        public static void PrintBreweries(List<GeoCode> trip, List<Brewery> breweries)
        {
            Console.WriteLine("Found {0} beer factories:", trip.Count - 2);
            int index = 0;
            double distance = 0;
            foreach (GeoCode geoCode in trip)
            {
                if (geoCode.Accuracy != "START")
                {
                    distance += geoCode.Coordinates.GetDistanceTo(trip[index - 1].Coordinates);
                    Console.WriteLine("       -> [{0}] {1}: {2:0.########}, {3:0.########} distance {4:0}km", geoCode.BreweryId, breweries.Find(x => x.Id == geoCode.BreweryId).Name, geoCode.Coordinates.Latitude, geoCode.Coordinates.Longitude, geoCode.Coordinates.GetDistanceTo(trip[index - 1].Coordinates) / 1000);
                }
                else if (index == trip.Count - 1)
                {
                    distance += geoCode.Coordinates.GetDistanceTo(trip[index - 1].Coordinates);
                    Console.WriteLine("       <- HOME: {0:0.########}, {1:0.########} distance {2:0}km", geoCode.Coordinates.Latitude, geoCode.Coordinates.Longitude, geoCode.Coordinates.GetDistanceTo(trip[index - 1].Coordinates) / 1000);
                }
                else
                {
                    Console.WriteLine("       -> HOME: {0:0.########}, {1:0.########} distance 0km", geoCode.Coordinates.Latitude, geoCode.Coordinates.Longitude);
                }
                index++;
            }
            Console.WriteLine();
            Console.WriteLine("Total distance travelled: {0:0}km", distance / 1000);
            Console.WriteLine();
        }
        public static void PrintProgramRuntime(System.Diagnostics.Stopwatch watch)
        {
            Console.WriteLine();
            Console.WriteLine("Program took: {0}ms", watch.ElapsedMilliseconds);
        }
    }
}
