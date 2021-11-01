using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;


namespace Plarium_Lesson9
{
    class Serialization
    {
       public static string pathManufacturers = "Manufacturers.json";
       public static string pathSouvenirs = "Souvenirs.json";

        public static void ManufacturersSerialization()
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(Dictionary<int, Manufacturer>));
            using(var file = new FileStream(pathManufacturers, FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(file, AddDelete.Manufacturers);
            }
        }
        public static void SouvenirsSerialization()
        {
            
            var jsonFormatter = new DataContractJsonSerializer(typeof(CollectionClass));
            using (var file = new FileStream(pathSouvenirs, FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(file, AddDelete.collectionClass);
            }

        }
         static Dictionary<int, Manufacturer> ManufacturersDeserialization()
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(Dictionary<int, Manufacturer>));
           
            using (var file = new FileStream(pathManufacturers, FileMode.Open))
            {
                return  jsonFormatter.ReadObject(file) as Dictionary<int, Manufacturer>;   
            } 

        }
        static CollectionClass SouvenirsDeserialization()
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(CollectionClass));

            using (var file = new FileStream(pathSouvenirs, FileMode.Open))
            {
                return jsonFormatter.ReadObject(file) as CollectionClass;   
            }
            /* string jsonstring = File.ReadAllText("Souvenirs.json");
             CollectionClass list = JsonSerializer.Deserialize<CollectionClass>(jsonstring);*/
        }
        public static void Deserialization()
        {
            if(File.Exists(pathManufacturers) && File.Exists(pathSouvenirs))
            {
                AddDelete.collectionClass = SouvenirsDeserialization();
                File.Delete(pathSouvenirs);
                AddDelete.Manufacturers = ManufacturersDeserialization();
                File.Delete(pathManufacturers);
            }
        }
    }
}
