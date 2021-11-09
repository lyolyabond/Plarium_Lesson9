using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Plarium_Lesson9
{
    class Function
    {
        /// <summary>
        /// Метод выводит информацию о сувенирах и их производителях
        /// </summary>
        public static void DisplayAllInformation()
        {
            //Проверка списка на пустоту
            if (AddDelete.collectionClass.Length() > 0)
            {
                using (var sw = new StreamWriter(Program.path, true))
                {
                    //Проход по элементам списка
                    foreach (Souvenir souvenir in AddDelete.collectionClass)
                    {
                        //Показ информации о сувенире
                        souvenir.DisplayInformationSouvenir();
                        //Проход по элементам словаря
                        foreach (KeyValuePair<int, Manufacturer> keyValue in AddDelete.Manufacturers)
                        {
                            //Если равны ключ и реквизиты производителя
                            if (keyValue.Key == souvenir.ManufacturerRequisites)
                            {
                                //Показ информации о производителе
                                AddDelete.Manufacturers[keyValue.Key].DisplayInformationManufacturer();
                                break;
                            }
                        }
                    }
                }
            }
            //Если список пуст
            else Console.WriteLine("Нет информации о сувенирах!");
        }

        /// <summary>
        /// Метод записывает информацию о сувенире по ключу в файл
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        static void KeyMatchingForList(int key, ref bool flag)
        {
            foreach (Souvenir value in AddDelete.collectionClass)
            {
                if (value.ManufacturerRequisites == key)
                {
                    using (var sw = new StreamWriter(Program.path, true))
                    {
                        value.WriteToFileInformationSouvenir(sw);
                    }
                    flag = true;
                }
            }
        }

        /// <summary>
        /// Метод записывает информацию о производителе по ключу в файл
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        static void KeyMatchingForDictionary(int key, ref bool flag)
        {
            if (AddDelete.Manufacturers.ContainsKey(key))
            {
                using (var sw = new StreamWriter(Program.path, true))
                {
                    AddDelete.Manufacturers[key].WriteToFileInformationManufacturer(sw);
                }
                flag = true;
            }
        }

        /// <summary>
        /// Метод записывает в файл и выводит информацию о сувенирах по названию производителя
        /// </summary>
        public static void DisplayInformationByManufacturer()
        {
            string name = Input.InputManufacturerName();
            bool flag = false;
            string header = $"Информация о сувенирах производителя {name}:";
            WritingHeaderToFile(header);

            //Механизм обработки исключительных ситуаций(если нет производителя с таким названием)
            try
            {
                //Проход по элементам словаря
                foreach (KeyValuePair<int, Manufacturer> keyValue in AddDelete.Manufacturers)
                {
                    //Проверка, есть ли такое название
                    if (string.Equals(keyValue.Value.ManufacturerName, name, StringComparison.OrdinalIgnoreCase))
                        KeyMatchingForList(keyValue.Key, ref flag);
                    //Запись информации по ключу
                }
                if (!flag)
                    throw new Exception();
            }
            catch (Exception)
            {
                WritingHeaderToFile($"Названия производителя {name} нет в базе!");
            }
            finally
            {
                WriteEmptyLine();
            }
            SearchTextInFile(header);
        }
        /// <summary>
        /// Метод записывает в файл и выводит информацию о сувенирах по названию страны производителя
        /// </summary>
        public static void DisplayInformationByCountry()
        {
            string country = Input.InputManufacturerCountry();
            bool flag = false;
            string header = $"Информация o сувенирах, произведенных в стране {country}:";
            WritingHeaderToFile(header);

            //Механизм обработки исключительных ситуаций(если нет страны с таким названием)
            try
            {
                //Проход по элементам словаря
                foreach (KeyValuePair<int, Manufacturer> keyValue in AddDelete.Manufacturers)
                {//Проверка, есть ли такое название страны
                    if (string.Equals(keyValue.Value.ManufacturerCountry, country, StringComparison.OrdinalIgnoreCase))
                        KeyMatchingForList(keyValue.Key, ref flag);
                    //Запись информации по ключу
                }
                if (!flag)
                    throw new Exception();
            }
            catch (Exception)
            {
                WritingHeaderToFile($"Названия страны {country} нет в базе!");
            }
            finally
            {
                WriteEmptyLine();
            }
            SearchTextInFile(header);

        }
        /// <summary>
        /// Метод записывает в файл и выводит информацию о производителях, чьи цены на сувениры меньше заданной
        /// </summary>
        public static void DisplayInformationByPrice()
        {
            decimal price = Input.InputPrice();
            bool flag = false;
            string header = $"Информация o производителях, чьи цены на сувениры меньше {price}:";
            WritingHeaderToFile(header);

            //Механизм обработки исключительных ситуаций(если нет цены на сувениры меньше заданной)
            try
            {
                //Проход по элементам списка
                foreach (Souvenir value in AddDelete.collectionClass)
                {
                    if (value.Price < price)
                    {
                        KeyMatchingForDictionary(value.ManufacturerRequisites, ref flag);
                        //Запись информации по ключу
                    }

                }
                if (!flag)
                    throw new Exception();
            }
            catch (Exception)
            {
                WritingHeaderToFile($"Сувенира с ценой, меньше чем {price} нет в базе!");
            }
            finally
            {
                WriteEmptyLine();
            }
            SearchTextInFile(header);
        }
        /// <summary>
        /// Метод записывает в файл и выводит информацию о производителях
        /// заданного сувенира, произведенного в заданном году
        /// </summary>
        public static void DisplayInformationByDate()
        {
            string souvenirName = Input.InputSouvenirName();
            int releaseDate = Input.InputReleaseDate();
            bool flag = false;
            string header = $"Информация о производителях сувенира {souvenirName}, произведенного в {releaseDate} году:";
            WritingHeaderToFile(header);

            //Механизм обработки исключительных ситуаций(если нет сувенира с заданным названием и датой)
            try
            {
                //Проход по элементам списка
                foreach (Souvenir value in AddDelete.collectionClass)
                {
                    //Если подходит под условия
                    if (string.Equals(value.SouvenirName, souvenirName, StringComparison.OrdinalIgnoreCase) && value.ReleaseDate == releaseDate)
                    {
                        KeyMatchingForDictionary(value.ManufacturerRequisites, ref flag);
                        //Запись информации по ключу
                    }
                }
                if (!flag)
                    throw new Exception();
            }
            catch (Exception)
            {
                WritingHeaderToFile($"Сувенира с названием {souvenirName} и датой выпуска {releaseDate} нет в базе!");
            }
            finally
            {
                WriteEmptyLine();
            }
            SearchTextInFile(header);
        }
        /// <summary>
        /// Метод меняет цену сувенира по ID
        /// </summary>
        /// <param name="flag"></param>
        public static void PriceChange(ref bool flag)
        {
            Console.Write("Введите ID сувенира: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Введите натуральное число: ");
            }

            //Механизм обработки исключительных ситуаций(если нет сувенира с заданным id)
            try
            {
                //Проход по элементам списка
                foreach (Souvenir value in AddDelete.collectionClass)
                {
                    //Если подходит под условия
                    if (value.ManufacturerRequisites == id)
                    {
                        decimal price = Input.InputPrice();
                        if (value.Price != price)
                        {
                            Database.ChangePrice(Menu.databaseName, value, price);
                            flag = true;
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели старую цену!");
                            return;
                        }
                    }
                }
                if (!flag)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine($"Сувенира с ID {id} нет в базе!");
            }
        }
        /// <summary>
        /// Метод оповещает об изменении цены
        /// </summary>
        /// <param name="flag"></param>
        public static void PriceChangeNotification(ref bool flag)
        {
            if (flag)
            {
                Console.WriteLine($"Вы изменили цену!");
                //Файл очищается, чтобы не хранить некорректную информацию
                File.WriteAllText(Program.path, String.Empty);
            }
        }

        /// <summary>
        /// Метод сортирует список по параметру, в зависимости от переданного делегата
        /// </summary>
        /// <param name="sortDelegate"></param>
        public static void SortList(EventDelegate.SortDelegate sortDelegate)
        {
            if (AddDelete.collectionClass.Length() > 0)
            {
                sortDelegate();
                Console.WriteLine("Список отсортирован!");
            }
            else Console.WriteLine("Списк пуст!");
        }
        /// <summary>
        /// Метод записывает заголовок запроса в файл
        /// </summary>
        /// <param name="header"></param>
        public static void WritingHeaderToFile(string header)
        {
            using (var sw = new StreamWriter(Program.path, true))
            {
                sw.WriteLine(header);
            }
        }
        /// <summary>
        /// Метод ищет по заголовку нужную информацию в файле и выводи на консоль
        /// </summary>
        /// <param name="str"></param>
        public static void SearchTextInFile(string str)
        {
            string line;
            using (var sw = new StreamReader(Program.path))
            {
                while ((line = sw.ReadLine()) != null)
                {
                    if (String.Equals(str, line))
                    {
                        Console.WriteLine("\n" + line);
                        while (!String.IsNullOrEmpty((line = sw.ReadLine())))
                            Console.WriteLine(line);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Метод записывает в файл пустую строку
        /// </summary>
        public static void WriteEmptyLine()
        {
            using (var sw = new StreamWriter(Program.path, true))
            {
                sw.WriteLine();
            }
        }
    }
}

