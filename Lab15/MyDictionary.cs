using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab15
{
  public  class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerator<KeyValuePair<TKey, TValue>>, ICloneable
    {
        private TKey[] _tableKeys = null;            //таблица ключей
        private TValue[] _tableValues = null;    // таблица значений
        private int _counter = 0;                          // счетчик 
        private int _capacity;
        /* Свойства объектов  */

        /// <summary>
        /// Данное значение перечислителя
        /// </summary>
        object IEnumerator.Current => Current;
        /// <summary>
        /// Таблица ключей
        /// </summary>
        public TKey[] Keys => _tableKeys;
        /// <summary>
        /// Таблица значений
        /// </summary>
        public TValue[] Values => _tableValues;
        /// <summary>
        /// Размер коллекции
        /// </summary>
        public int Capacity => _capacity;
        /// <summary>
        /// Количество элементов в коллекции
        /// </summary>
        public int Count => _counter;
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public MyDictionary()
        {
            _tableKeys = new TKey[8];
            _tableValues = new TValue[8];
        }
        /// <summary>
        /// Удаление всех элементов из коллекции
        /// </summary>
        public void Clear()
        {
            _tableKeys = null;
            _tableValues = null;
            _counter = 0;
        }
        /// <summary>
        ///  Клонирование коллекции
        /// </summary>
        public object Clone()
        {
            MyDictionary<TKey, TValue> cloneDictionary = new MyDictionary<TKey, TValue>(_counter);
            for (int i = 0; i < _tableKeys.Length; i++)
                cloneDictionary.Add(_tableKeys[i], _tableValues[i]);
            return cloneDictionary;
        }
        /// <summary>
        /// Удаление первого вхождения элемента коллекции
        /// </summary>
        /// <param name="value">Значение для удаления</param>
        public void Remove(TValue value)
        {
            for (int i = 0; i < _tableKeys.Length; i++)
            {
                if (Equals(_tableValues[i], value))
                {
                    Array.Clear(_tableKeys, i, 1);
                    Array.Clear(_tableValues, i, 1);
                    _counter--;
                    break;
                }
            }
        }
        /// <summary>
        /// Инциализация коллекции
        /// </summary>
        /// <param name="dictionary">входная коллекция</param>
        public MyDictionary(MyDictionary<TKey, TValue> dictionary)
        {
            this._tableKeys = dictionary._tableKeys;
            this._tableValues = dictionary._tableValues;
            this._capacity = dictionary._capacity;
            this._counter = dictionary._counter;
        }
        /// <summary>
        /// Инициализация коллекции заданого размера
        /// </summary>
        /// <param name="capacity">размер который может вместить коллекция</param>
        public MyDictionary(int capacity)
        {
            this._capacity = capacity;
            this._tableKeys = new TKey[capacity];
            this._tableValues = new TValue[capacity];
        }
        /// <summary>
        /// Добавление элемента в словарь
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        public void Add(TKey key, TValue value)
        {
            this._counter++;
            Array.Resize(ref _tableKeys, _counter);
            _tableKeys[_counter - 1] = key;
            Array.Resize(ref _tableValues, _counter);
            _tableValues[_counter - 1] = value;
        }
        /// <summary>
        /// Индексатор словаря доступа к элементам
        /// </summary>
        /// <param name="key">Ключ для получения доступа к элементу словаря</param>
        public TValue this[TKey key]
        {
            get
            {
                int ind = 0;
                for (int i = 0; i < this._tableKeys.Length; i++)
                {
                    if (Equals(_tableKeys[i], key))
                    {
                        ind = i;
                        return _tableValues[ind];
                    }
                }
                return _tableValues[ind];
            }
        }
        /// <summary>
        /// Проверка вхождения ключа в словаре
        /// </summary>
        /// <param name="key">ключ в словаре</param>
        /// <returns>результат проверки вхождения</returns>
        public bool ContainsKey(TKey key)
        {
            for (var index = 0; index < _tableKeys.Length; index++)
            {
                TKey t = _tableKeys[index];
                if (Equals(t, key))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Проверяет вхождение элемента в словаре
        /// </summary>
        /// <param name="value">Значение в словаре</param>
        /// <returns>результат проверки вхождения</returns>
        public bool ContainsValue(TValue value)
        {
            for (var index = 0; index < _tableValues.Length; index++)
            {
                var t = _tableValues[index];
                if (Equals(t, value))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Приведение словаря к списку элементов
        /// </summary>
        /// <returns></returns>
        public List<TValue> ValtuestoList()
        {
            List<TValue> list = new List<TValue>();
            for (int i = 0; i < _tableValues.Length; i++)
                list.Add(_tableValues[i]);
            return list;
        }
        /// <summary>
        /// Приведение таблицы ключей к списку
        /// </summary>
        /// <returns></returns>
        public List<TKey> KeystoList()
        {
            List<TKey> list = new List<TKey>();
            for (int i = 0; i < _tableKeys.Length; i++)
                list.Add(_tableKeys[i]);
            return list;
        }
        /// <summary>
        /// Сортировка словаря
        /// </summary>
        public void Sort()
        {
            Array.Sort(_tableValues, _tableKeys);
            Array.Reverse(_tableKeys);
            Array.Reverse(_tableValues);
        }

        /// <summary>
        /// Перечислитель словаря
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this._tableKeys.Select((t, i) => new KeyValuePair<TKey, TValue>(_tableKeys[i], _tableValues[i])).GetEnumerator();
        /// <summary>
        /// Очищение списка из памяти
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(this);
        #region Disable
        public bool MoveNext() => throw new NotImplementedException();
        public void Reset() => throw new NotImplementedException();
        public KeyValuePair<TKey, TValue> Current { get; }

        #endregion //Методы которые не
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();



    }
}
