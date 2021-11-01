using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;


namespace Plarium_Lesson9
{
    //Тип определяет контракт данных и может быть сериализован
    [DataContract]
    class VIPGift : Souvenir
    {
        //Реализация абстрактного свойства
        public override string KindOfSouvenir { get; set; } = "VIP сувенир";
        [DataMember]
        public string Occasion { get; set; }
        public VIPGift(string souvenirName, string occasion,  int releaseDate, decimal price)
            : base(souvenirName, releaseDate, price)
        {
            Occasion = occasion;
        }
        public VIPGift()
        { }
        /// <summary>
        /// Переопределённый метод для записи информации в файл
        /// </summary>
        /// <param name="sw"></param>
        public override void WriteToFileInformationSouvenir(StreamWriter sw)
        {
            base.WriteToFileInformationSouvenir(sw);
            sw.WriteLine($"Повод для подарка: {Occasion}");
            sw.WriteLine("--------------------------");
        }
        /// <summary>
        /// Переопределённый метод для записи информации в БД
        /// </summary>
        /// <param name="sw"></param>
        public override void WriteToDatabase(StreamWriter sw)
        {
            base.WriteToDatabase(sw);
            sw.Write($"{this.Occasion},");
        }
        /// <summary>
        /// Переопределённый метод для вывода информации в консоль
        /// </summary>
        public override void DisplayInformationSouvenir()
        {
            base.DisplayInformationSouvenir();
            Console.WriteLine($"Повод для подарка: {Occasion}");
            Console.WriteLine("--------------------------");
        }
    }
}
