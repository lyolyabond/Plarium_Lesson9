using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Plarium_Lesson9
{
    class Database
    {
        public static void CreateDatabase(string path)
        {
            path += ".txt";
            if (File.Exists(path))
            {
                Console.WriteLine("Файл уже существует!");
            }
            else
            {
                File.Create(path).Close();
                Console.WriteLine($"Файл {path} успешно создан!");
            }
        }

        public static void ReadDatabase(string path)
        {
            path += ".txt";
            if (File.Exists(path))
            {
                using (var sr = new StreamReader(path))
                {
                    if(new FileInfo(path).Length > 0)
                    { 
                    while(!sr.EndOfStream)
                    {
                        Console.WriteLine(sr.ReadLine());
                    }
                    }
                    else Console.WriteLine($"Файл пустой!");
                }
            }
            else Console.WriteLine("Такого файла не существует!");
        }


        public static void UpdateDatabase(CollectionClass souvenirs, string path)
        {
            path += ".txt";
            if (souvenirs.Length() > 0 && File.Exists(path))
            {
                using (var sw = new StreamWriter(path, false))
                { 
                    foreach (Souvenir souvenir in souvenirs)
                    {
                        souvenir.WriteToDatabase(sw);
                       if(AddDelete.Manufacturers.ContainsKey(souvenir.ManufacturerRequisites))
                        {
                                sw.Write($"{AddDelete.Manufacturers[souvenir.ManufacturerRequisites].ManufacturerName},");
                                sw.WriteLine($"{AddDelete.Manufacturers[souvenir.ManufacturerRequisites].ManufacturerCountry}");
                        }
                    }
                }
                Console.WriteLine($"Файл обновлён!");
            }
            else Console.WriteLine("Такого файла не существует или список пуст!");
        }
        public static void DeleteDatabase(string path)
        {
            path += ".txt";
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine($"Файл удалён!");
            }
            else Console.WriteLine("Такого файла не существует!");
        }

    }
}
