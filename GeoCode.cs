using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    class GeoCode : ISetData<GeoCode>
    {
        public int Id { get; set; }
        public int BreweryId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Accuracy { get; set; }
        
        public GeoCode() { }

        public GeoCode(int id, int breweryId, decimal latitude, decimal longitude, string accuracy)
        {
            Id = id;
            BreweryId = breweryId;
            Latitude = latitude;
            Longitude = longitude;
            Accuracy = accuracy;
        }

        public void SetData(string[] data)
        {
            Id = int.Parse(data[0]);
            BreweryId = int.Parse(data[1]);
            Latitude = decimal.Parse(data[2]);
            Longitude = decimal.Parse(data[3]);
            Accuracy = data[4];
        }
    }
}
