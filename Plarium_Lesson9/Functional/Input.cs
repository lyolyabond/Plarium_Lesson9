using System;
using System.Collections.Generic;
using System.Text;

namespace Plarium_Lesson9
{
    class Input
    {
        //Методы для ввода информации
        public static string InputSouvenirName()
        { 
            Console.Write("Введите название сувенира: ");
           return Console.ReadLine();
        }
        public static int InputReleaseDate()
        {
            Console.Write("Введите год выпуска: ");
            int releaseDate;
            while (!int.TryParse(Console.ReadLine(), out releaseDate) || releaseDate > 2021)
            {
                Console.Write("Введите год в формате 2021: ");
            }
            return releaseDate;
        }
        public static decimal InputPrice()
        {
            Console.Write("Введите цену: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Введите цену в формате 105,62 или 105: ");
            }
            return price;
        }
        public static string InputManufacturerName()
        {
            Console.Write("Введите название производителя: ");
            return Console.ReadLine();
        }
        public static string InputManufacturerCountry()
        {
            Console.Write("Введите страну производителя: ");
            return Console.ReadLine();
        }
    }
}
