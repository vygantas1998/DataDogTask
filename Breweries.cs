using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    class Breweries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress1 { get; set; }
        public string Adress2 { get; set; }
        public string City { get; set; }

        public Breweries()
        {
        }

        public Breweries(int id, string name, string adress1, string adress2, string city)
        {
            Id = id;
            Name = name;
            Adress1 = adress1;
            Adress2 = adress2;
            City = city;
        }
    }
}
