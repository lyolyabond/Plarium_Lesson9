using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Plarium_Lesson9
{
    //Класс производителя сувениров
    [DataContract]
    class Manufacturer
    {
        public event EventHandler <EventDelegate.KeyEventArgs> ManufacturerRemoved;
        //Инициализирующий конструктор

        public Manufacturer(string manufacturerName, string manufacturerCountry)
        {
            ManufacturerName = manufacturerName;
            ManufacturerCountry = manufacturerCountry;
        }
        //Конструктор по умолчанию
        public Manufacturer()
        { }
        [DataMember]
        public string ManufacturerName { get; set; }
        [DataMember]
        public string ManufacturerCountry { get; set; }
        public void WriteToFileInformationManufacturer(StreamWriter sw)
        {
            sw.WriteLine($"Название производителя: {ManufacturerName}");
            sw.WriteLine($"Страна производителя: {ManufacturerCountry}");
            sw.WriteLine("--------------------------");
        }
        public void DisplayInformationManufacturer()
        {
            Console.WriteLine($"Название производителя: {ManufacturerName}");
            Console.WriteLine($"Страна производителя: {ManufacturerCountry}");
            Console.WriteLine("--------------------------");
        }

        //Метод вызывает событие
        protected virtual void OnManufacturerRemoved(EventDelegate.KeyEventArgs e)
        {
            var temp = ManufacturerRemoved;
            //Вызов события, проверка на null перед вызовом
            temp?.Invoke(this, e);
        }
        public void RemoveManufacturer(int key)
        {
            var e = new EventDelegate.KeyEventArgs(key);
            OnManufacturerRemoved(e);
        }

    }
}
