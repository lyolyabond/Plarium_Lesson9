using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;


namespace Plarium_Lesson9
{
    [DataContract]
    class ThematicSouvenir : Souvenir
    {
        //Реализация абстрактного свойства

        public override string KindOfSouvenir { get; set; } = "Тематический сувенир";
        [DataMember]
        public string SubjectMatter { get; set; }
        public ThematicSouvenir(string souvenirName, string subjectMatter,  int releaseDate, decimal price)
            : base(souvenirName, releaseDate, price)
        {
            SubjectMatter = subjectMatter;
        }
        public ThematicSouvenir()
        { }
        //Переопределение метода
        public override void WriteToFileInformationSouvenir(StreamWriter sw)
        {
            base.WriteToFileInformationSouvenir(sw);
            sw.WriteLine($"Название тематики сувенира: {SubjectMatter}");
            sw.WriteLine("--------------------------");
        }
        public override void WriteToDatabase(StreamWriter sw)
        {
            base.WriteToDatabase(sw);
            sw.Write($"{this.SubjectMatter},");
        }
        public override void DisplayInformationSouvenir()
        {
            base.DisplayInformationSouvenir();
            Console.WriteLine($"Название тематики сувенира: {SubjectMatter}");
            Console.WriteLine("--------------------------");
        }
    }
}
