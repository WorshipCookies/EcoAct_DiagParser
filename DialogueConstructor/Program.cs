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
using System.Xml.Serialization;

namespace DialogueConstructor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string input_path = System.AppDomain.CurrentDomain.BaseDirectory + "Scene Location\\";
            string output_path = System.AppDomain.CurrentDomain.BaseDirectory + "Output\\";

            Console.WriteLine("----------------- Make Sure All Scene Files are in the Scene Location Folder -------------\n\n");

            foreach (string filename in Directory.GetFiles(input_path))
            {
                Console.WriteLine("Reading File in " + filename);

                TextReader tr = new TextReader(filename);
                Scene scene = tr.TextParser(1);

                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
                var jsonVal = Newtonsoft.Json.JsonConvert.SerializeObject(scene, settings);
                string outputfile = System.IO.Path.GetFileName(filename);

                File.WriteAllText(output_path + outputfile.Split('.')[0] + ".json", jsonVal.ToString());

                Console.WriteLine("Finished with File " + outputfile + ".\n\n");
            }

            Console.WriteLine("\nAll Processing was Completed. \nCheck Folder: " + output_path);
        }
    }
}
