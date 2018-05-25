using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    class Brewery : ISetData<Brewery>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress1 { get; set; }
        public string Adress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public int AddUser { get; set; }
        public string LastMod { get; set; }

        public Brewery() { }

        public Brewery(int id, string name, string adress1, string adress2, string city, string state, string code, string country, string phone, string website, string filePath, string description, int addUser, string lastMod)
        {
            Id = id;
            Name = name;
            Adress1 = adress1;
            Adress2 = adress2;
            City = city;
            State = state;
            Code = code;
            Country = country;
            Phone = phone;
            Website = website;
            FilePath = filePath;
            Description = description;
            AddUser = addUser;
            LastMod = lastMod;
        }

        public void SetData(string[] data)
        {
            Id = int.Parse(data[0]);
            Name = data[1];
            Adress1 = data[2];
            Adress2 = data[3];
            City = data[4];
            State = data[5];
            Code = data[6];
            Country = data[7];
            Phone = data[8];
            Website = data[9];
            FilePath = data[10];
            Description = data[11];
            AddUser = int.Parse(data[12]);
            LastMod = data[13];
        }
    }
}
