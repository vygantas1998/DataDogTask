using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataDog
{
    class ReadData<T> where T : ISetData<T>, new()
    {
        public static List<T> ReadDataFromWeb(string url, string filename)
        {
            List<T> objList = new List<T>();

            WebClient client = new WebClient();

            string filePath = @"C:\Users\vygan\Desktop\DataDog\DataDog\Files\" + filename;

            client.DownloadFile(url, filePath);
            string file = client.DownloadString(url);

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");
                parser.TrimWhiteSpace = true;
                parser.ReadLine();
                while (!parser.EndOfData)
                {
                    string[] data = parser.ReadFields();
                    T obj = new T();
                    obj.SetData(data);
                    objList.Add(obj);
                }
            }
            return objList;
        }
    }
}
