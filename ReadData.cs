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
        public static List<T> ReadDataFromWeb(string url)
        {
            List<T> objList = new List<T>();

            WebClient client = new WebClient();
            string file = client.DownloadString(url);
            string[] lines = new Regex(@"(?m)^[^""\r\n]*(?:(?:""[^""]*"")+[^""\r\n]*)*")
                .Matches(file).Cast<Match>().Select(m => m.Value).ToArray();

            foreach (string line in lines)
            {
                if (line != lines[0] && line.Length != 0)
                {
                    string[] data = new Regex("((?<=\")[^\"]*(?=\"(,|$)+)|(?<=,|^)[^,\"]*(?=,|$))")
                        .Matches(line).Cast<Match>().Select(m => m.Value).ToArray();
                    T obj = new T();
                    obj.SetData(data);
                    objList.Add(obj);
                }
            }
            return objList;
        }
    }
}
