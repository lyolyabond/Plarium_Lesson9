using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;


namespace Plarium_Lesson9
{
    [DataContract]
    class PromotionalSouvenir : Souvenir
    {
        //Реализация абстрактного свойства

        public override string KindOfSouvenir { get; set; } = "Промосувенир";
        [DataMember]
        public string CompanyName { get; set; }
        public PromotionalSouvenir(string souvenirName, string companyName, int releaseDate, decimal price)
            : base(souvenirName,  releaseDate, price)
        {
            CompanyName = companyName;
        }
        public PromotionalSouvenir()
        { }
        //Переопределение метода
        public override void WriteToFileInformationSouvenir(StreamWriter sw)
        {
            base.WriteToFileInformationSouvenir(sw);
            sw.WriteLine($"Название компании промосувенира: {CompanyName}");
            sw.WriteLine("--------------------------");
        }
        public override void WriteToDatabase(StreamWriter sw)
        {
            base.WriteToDatabase(sw);
            sw.Write($"{this.CompanyName},");
        }
        public override void DisplayInformationSouvenir()
        {
            base.DisplayInformationSouvenir();
            Console.WriteLine($"Название компании бизнес-сувенира: {CompanyName}");
            Console.WriteLine("--------------------------");
        }
    }
}
