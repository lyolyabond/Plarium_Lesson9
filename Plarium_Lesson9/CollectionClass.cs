using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

//Для хранения коллекций объектов предметной области использовать
//обобщенные коллекции (для одной из сущностей использовать коллекцию типа СЛОВАРЬ).

//Коллекцию сущностей представить в виде класса (коллекция - поле класса). 
//Реализовать индексаторы и итераторы по элементам коллекции.

namespace Plarium_Lesson9
{
    //Коллекция сущностей(список) представлена в виде класса 
   
    [DataContract]
    [KnownType(typeof(BusinessSouvenir))]
    [KnownType(typeof(PromotionalSouvenir))]
    [KnownType(typeof(ThematicSouvenir))]
    [KnownType(typeof(VIPGift))]
    class CollectionClass 
    {
        //Список объектов класса сувенира
        [DataMember]
        List<Souvenir> souvenirs;
        public CollectionClass()
        {
            souvenirs = new List<Souvenir>();
        }
        
        //Индексатор по элементам коллекции
        public Souvenir this[int index]
        {
            get 
            {//Возвращает объект класса по индексу в списке(если не пустой)
                if (souvenirs.Count > 0)
                   return souvenirs[index];
                else return null;
            }
        }
        //Итератор по элементам коллекции
        public IEnumerator GetEnumerator()
        {
            for(int i = 0; i < souvenirs.Count; i++)
            {
                yield return souvenirs[i];
            }
        }
        //Метод, который возвращает количество элементов в списке
        public int Length()
        {
            return souvenirs.Count;
        }
        //Метод удаления элемента списка по индексу
        public void Remove(int key)
        {
            if(souvenirs.Count > 0)
            souvenirs.RemoveAt(key);
        }
        //Метод добавления объекта в список
        public void Add(Souvenir souvenir)
        {
            souvenirs.Add(souvenir);
        }
        //Очищение списка
        public void Clear()
        {
            souvenirs.Clear();
        } 
        //Сортировка списка по цене
        public void SortByPrice()=> souvenirs.Sort(new ComparerByPrice());
        //Сортировка списка по названию
        public void SortBySouvenirName() => souvenirs.Sort(new ComparerBySouvenirName());

        //Подписчик на событие ManufacturerRemoved - удаление производителя
        //Метод при возникновении события удаления производителя, удаляет его сувениры
        public void DeleteObjectsByKey (object source, EventDelegate.KeyEventArgs keyEventArgs)
        {
            bool flag = false;
            for (int i = 0; i < this.Length(); i++)
            {
                if (this[i].ManufacturerRequisites == keyEventArgs.Key)
                {
                    //Удаление элемента по индексу из списка сувениров
                    this.Remove(i);
                    flag = true;
                }
            }
            if(flag)
            {
                Console.WriteLine($"Удаление сувенира с ID {keyEventArgs.Key} прошло успешно!");
            }
        }

    }
}
