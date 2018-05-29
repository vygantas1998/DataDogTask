using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    class GeoCode : ISetData<GeoCode>
    {
        public int Id { get; set; }
        public int BreweryId { get; set; }
        public GeoCoordinate Coordinates { get; set; }
        public string Accuracy { get; set; }
        
        public GeoCode() { }

        public GeoCode(int id, int breweryId, double latitude, double longitude, string accuracy)
        {
            Id = id;
            BreweryId = breweryId;
            Coordinates = new GeoCoordinate(latitude, longitude);
            Accuracy = accuracy;
        }

        public void SetData(string[] data)
        {
            Id = int.Parse(data[0]);
            BreweryId = int.Parse(data[1]);
            Coordinates = new GeoCoordinate(double.Parse(data[2]), double.Parse(data[3]));
            Accuracy = data[4];
        }
        public override string ToString()
        {
            return Coordinates.ToString();
        }
    }
}
