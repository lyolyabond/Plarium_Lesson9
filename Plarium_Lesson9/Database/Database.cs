using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Plarium_Lesson9
{
    class Database
    {
        static string pathTemporary = "Temporary.txt";
        /// <summary>
        /// Метод создаёт БД по указаному названию, если она ещё не существует
        /// </summary>
        /// <param name="path"></param>
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
        /// <summary>
        /// Метод выводит в консоль информацию из БД
        /// </summary>
        /// <param name="path"></param>
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
        /// <summary>
        /// Метод добавляет запись о сувенире в БД
        /// </summary>
        /// <param name="path"></param>
        public static void AddSouvenirToDatabase(string path)
        {
            path += ".txt";
            int index = AddDelete.collectionClass.Length() - 1;
            if (AddDelete.collectionClass.Length() > 0 && File.Exists(path))
            {
                using (var sw = new StreamWriter(path, true))
                {
                    AddDelete.collectionClass[index].WriteToDatabase(sw);
                }
            }
        }
        /// <summary>
        /// Метод добавляет запись о производителе в БД
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        public static void AddManufacturerToDatabase(int id, string path)
        {
            path += ".txt";
            if (AddDelete.collectionClass.Length() > 0 && File.Exists(path))
            {
                using (var sw = new StreamWriter(path, true))
                {
                    AddDelete.Manufacturers[id].WriteToDatabase(sw);
                }
             }
        }
        /// <summary>
        /// Метод обновляет содержимое БД
        /// </summary>
        /// <param name="souvenirs"></param>
        /// <param name="path"></param>
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
                            AddDelete.Manufacturers[souvenir.ManufacturerRequisites].WriteToDatabase(sw);
                        }
                    }
                }
                Console.WriteLine($"Файл обновлён!");
            }
            else Console.WriteLine("Такого файла не существует или список пуст!");
        }
        /// <summary>
        /// Метод меняет цену в БД 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="souvenir"></param>
        /// <param name="newPrice"></param>
        public static void ChangePrice(string path, Souvenir souvenir, decimal newPrice)
        {
            path += ".txt";
            
            if (File.Exists(path))
            {
                string text;
                using (var sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                }
                
                string oldRecord = souvenir.Record();
                if (text.Contains(oldRecord))
                {
                    souvenir.Price = newPrice;
                    string newText = text.Replace(oldRecord, souvenir.Record());
                    using (var sw = new StreamWriter(pathTemporary))
                    {
                        sw.Write(newText);
                    }
                      
                    File.Delete(path);
                    File.Move(pathTemporary, path);
                }
            }
        }
        /// <summary>
        /// Метод удаляет запись из БД по ключу
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key"></param>
        public static void DeleteRecord(string path, int key)
        {
            path += ".txt"; 
            if (File.Exists(path))
            {
                var lines = File.ReadLines(path).Where(l => !l.Contains(";" + key.ToString() + ";"));
                File.WriteAllLines(pathTemporary, lines);
                File.Delete(path);
                File.Move(pathTemporary, path);
            }
        }
        /// <summary>
        /// Метод удаляет указанную БД, если она существует
        /// </summary>
        /// <param name="path"></param>
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
