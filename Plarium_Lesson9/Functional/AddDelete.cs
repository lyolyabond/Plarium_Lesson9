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

        //Метод добавления объекта производителя в словарь
        public static void AddManufacturer(Manufacturer manufacturer)
        {
            Manufacturers.Add(ID, manufacturer);
            File.WriteAllText(Program.path, String.Empty);
        }
        //Метод для удаления элементов массива по заданному названию производителя
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
                    if (keyValue.Value.ManufacturerName == name)
                    {
                        //Удаление элемента по ключу из словаря производителей
                        Manufacturers.Remove(keyValue.Key);
                        //Метод, который вызывает событие
                        eventDelete.RemoveManufacturer(keyValue.Key);
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
        //Метод очищения коллекций
        public static void ClearCollections()
        {
            collectionClass.Clear();
            Manufacturers.Clear();
            Console.WriteLine("Коллекции очищены.");
            File.WriteAllText(Program.path, String.Empty);
        }
    }   
}
