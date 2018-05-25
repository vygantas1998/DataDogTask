using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    class Beer : ISetData<Beer>
    {
        public int Id { get; set; }
        public int BreweryId { get; set; }
        public string Name { get; set; }
        public int CatId { get; set; }
        public int StyleId { get; set; }
        public decimal Abv { get; set; }
        public float Ibu { get; set; }
        public float Srm { get; set; }
        public int Upc { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public int AddUser { get; set; }
        public string LastMod { get; set; }

        public Beer() { }

        public Beer(int id, int breweryId, string name, int catId, int styleId,
            decimal abv, float ibu, float srm, int upc, string filePath,
            string description, int addUser, string lastMod)
        {
            Id = id;
            BreweryId = breweryId;
            Name = name;
            CatId = catId;
            StyleId = styleId;
            Abv = abv;
            Ibu = ibu;
            Srm = srm;
            Upc = upc;
            FilePath = filePath;
            Description = description;
            AddUser = addUser;
            LastMod = lastMod;
        }

        public void SetData(string[] data)
        {
            Id = int.Parse(data[0]);
            BreweryId = int.Parse(data[1]);
            Name = data[2];
            CatId = int.Parse(data[3]);
            StyleId = int.Parse(data[4]);
            Abv = decimal.Parse(data[5]);
            Ibu = float.Parse(data[6]);
            Srm = float.Parse(data[7]);
            Upc = int.Parse(data[8]);
            FilePath = data[9];
            Description = data[10];
            AddUser = int.Parse(data[11]);
            LastMod = data[12];
        }
    }
}
