using System;
using System.Collections.Generic;
using System.Text;

namespace Plarium_Lesson9
{
    //Компараторы объектов Souvenir

    class ComparerByPrice : IComparer<Souvenir>
    {
        //Сравнивает объекты в зависимости от цены значение свойства Price
        public int Compare(Souvenir souvenir1, Souvenir souvenir2)=> souvenir1.Price.CompareTo(souvenir2.Price);
    }
    class ComparerBySouvenirName : IComparer<Souvenir>
    {
        //Сравнивает объекты в зависимости от названия сувенира(чтобы отсортировать в алфавитном порядке)
        public int Compare(Souvenir souvenir1, Souvenir souvenir2) => 
            String.Compare(souvenir1.SouvenirName, souvenir2.SouvenirName, StringComparison.CurrentCultureIgnoreCase);
    }
}
