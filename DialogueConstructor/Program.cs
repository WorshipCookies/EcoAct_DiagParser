using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace DialogueConstructor
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Scene Location\\";
            Console.WriteLine(path);

            string filename = "Scene2";

            TextReader tr = new TextReader(path + filename + ".txt");
            Scene scence = tr.TextParser(1);
            var jsonVal = Newtonsoft.Json.JsonConvert.SerializeObject(scence);

            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "Output\\" + filename + ".json", jsonVal.ToString());
        }
    }
}
