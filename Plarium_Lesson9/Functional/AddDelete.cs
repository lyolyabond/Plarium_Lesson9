using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Plarium_Lesson9
{
    class AddDelete
    {
        //Переменная, которая хранит последний ключ
        public static int ID = 0;
        //Коллекция, которая хранит ключи и объекты класса производителя
        public static Dictionary<int, Manufacturer> Manufacturers = new Dictionary<int, Manufacturer>();
        //Объект класса для работы со списком сувениров
        public static CollectionClass collectionClass = new CollectionClass();


        /// <summary>
        /// Метод добавляет объект производителя в словарь
        /// </summary>
        /// <param name="manufacturer"></param>
        public static void AddManufacturer(Manufacturer manufacturer)
        {
            Manufacturers.Add(ID, manufacturer);
            //Файл очищается, чтобы не хранить некорректную информацию
            File.WriteAllText(Program.path, String.Empty);
            Database.AddManufacturerToDatabase(ID, Menu.databaseName);
        }
        /// <summary>
        /// Метод удаляет объект из словаря по заданному названию производителя
        /// </summary>
        /// <param name="eventDelete"></param>
        public static void DeleteItemByManufacturer(Manufacturer eventDelete)
        {
            Console.Write("Введите название производителя: ");
            string name = Console.ReadLine();
            
            bool flag = false;

            //Механизм обработки исключительных ситуаций(если нет сувенира с заданным названием производителя)
            try
            {
                //Проход по элементам словаря
                foreach (KeyValuePair<int, Manufacturer> keyValue in Manufacturers)
                {//Проверка, есть ли такое название
                    if (string.Equals(keyValue.Value.ManufacturerName, name, StringComparison.OrdinalIgnoreCase))
                    {
                        //Удаление элемента по ключу из словаря производителей
                        Manufacturers.Remove(keyValue.Key);
                        //Метод, который вызывает событие
                        eventDelete.RemoveManufacturer(keyValue.Key);
                        //Файл очищается, чтобы не хранить некорректную информацию
                        File.WriteAllText(Program.path, String.Empty);
                        flag = true;
                    }
                }
                if(!flag)
                 throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine($"Производителя с названием {name} нет в базе!");
            }
    
        }
        /// <summary>
        /// Метод очищает коллекции
        /// </summary>
        public static void ClearCollections()
        {
            collectionClass.Clear();
            Manufacturers.Clear();
            Console.WriteLine("Коллекции очищены.");
            //Файл очищается, чтобы не хранить некорректную информацию
            File.WriteAllText(Program.path, String.Empty);
            File.WriteAllText(Menu.databaseName +".txt", String.Empty);
        }
    }   
}
