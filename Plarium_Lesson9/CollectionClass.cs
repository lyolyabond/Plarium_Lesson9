using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;

//Для хранения коллекций объектов предметной области использовать
//обобщенные коллекции (для одной из сущностей использовать коллекцию типа СЛОВАРЬ).

//Коллекцию сущностей представить в виде класса (коллекция - поле класса). 
//Реализовать индексаторы и итераторы по элементам коллекции.

namespace Plarium_Lesson9
{
    
    //Тип определяет контракт данных и может быть сериализован
    [DataContract]
    //Типы, которые следует включить при десериализации
    [KnownType(typeof(BusinessSouvenir))]
    [KnownType(typeof(PromotionalSouvenir))]
    [KnownType(typeof(ThematicSouvenir))]
    [KnownType(typeof(VIPGift))]
    //Коллекция сущностей(список) представлена в виде класса 
    class CollectionClass 
    {
        //Список объектов класса сувенира
        [DataMember]
        List<Souvenir> souvenirs;
        public CollectionClass()
        {
            souvenirs = new List<Souvenir>();
        }

        /// <summary>
        /// Индексатор по элементам списка сувениров
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Souvenir this[int index]
        {
            get 
            {
                //Возвращает объект класса по индексу в списке(если не пустой)
                if (souvenirs.Count > 0)
                   return souvenirs[index];
                else return null;
            }
        }
        /// <summary>
        /// Итератор по элементам списка сувениров
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            for(int i = 0; i < souvenirs.Count; i++)
            {
                yield return souvenirs[i];
            }
        }
        /// <summary>
        /// Метод возвращает количество элементов в списке
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return souvenirs.Count;
        }
        /// <summary>
        /// Метод удаляет элемент списка по индексу
        /// </summary>
        /// <param name="key"></param>
        public void Remove(int key)
        {
            if(souvenirs.Count > 0)
            souvenirs.RemoveAt(key);
        }
        /// <summary>
        /// Метод добавляет объект в список
        /// </summary>
        /// <param name="souvenir"></param>
        public void Add(Souvenir souvenir)
        {
            souvenirs.Add(souvenir);
            Database.AddSouvenirToDatabase(Menu.databaseName);
        }
        /// <summary>
        /// Метод очищает список
        /// </summary>
        public void Clear()
        {
            souvenirs.Clear();
        } 
        /// <summary>
        /// Метод сортирует список по цене
        /// </summary>
        public void SortByPrice()=> souvenirs.Sort(new ComparerByPrice());
        /// <summary>
        /// Метод сортирует список по названию
        /// </summary>
        public void SortBySouvenirName() => souvenirs.Sort(new ComparerBySouvenirName());

        /// <summary>
        /// Метод при возникновении события удаления производителя, удаляет его сувениры
        /// Подписчик на событие ManufacturerRemoved - удаление производителя
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyEventArgs"></param>
        public void DeleteObjectsByKey (object source, EventDelegate.KeyEventArgs keyEventArgs)
        {
            bool flag = false;
            for (int i = 0; i < this.Length(); i++)
            {
                if (this[i].ManufacturerRequisites == keyEventArgs.Key)
                {
                    //Удаление элемента по индексу из списка сувениров
                    this.Remove(i);
                    Database.DeleteRecord(Menu.databaseName, keyEventArgs.Key);
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
