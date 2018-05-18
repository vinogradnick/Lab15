using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hierarhy;


namespace Lab15
{
    class Program
    {
        static Random random = new Random();
        static MyDictionary<string, MyDictionary<Person, string>> Factory =new MyDictionary<string, MyDictionary<Person, string>>();
        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                Factory.Add(Path.GetRandomFileName(), Generate());
            }

            Print(Factory);
            Console.WriteLine("\n");
            //Console.WriteLine("1-Запрос------------------------------------------\\");

            //SearchNameWorkersInFactory(Factory.Keys[2]);
            //Console.WriteLine("Для перехода к следующему запросу нажмите любую клавишу");
            //Console.ReadLine();

            //Console.WriteLine("2-Запрос------------------------------------------\\");
            //GetCountEngineersInFactory(Factory.Keys[2]);
            //Console.WriteLine("Для перехода к следующему запросу нажмите любую клавишу");
            //Console.ReadLine();

            //Console.WriteLine("3-Запрос------------------------------------------\\");
            //GetAgeAllWorkersInFactory(Factory.Keys[2]);
            //Console.WriteLine("Для перехода к следующему запросу нажмите любую клавишу");
            //Console.ReadLine();

            //Console.WriteLine("4-Запрос------------------------------------------\\");
            //OperationOnCollectionLinq(Factory.Keys[2],Factory.Keys[1]);
            //Console.WriteLine("Для перехода к следующему запросу нажмите любую клавишу");
            //Console.ReadLine();

            Console.WriteLine("5-Запрос------------------------------------------\\");
            RemoveWorkers(40,Factory.Keys[1],Factory.Keys[2]);
            Console.WriteLine("Для перехода к следующему запросу нажмите любую клавишу");
            Console.ReadLine();



            Console.ReadKey();
        }
        #region Выборка [Выбор имен сотрудников в цехе]
        /// <summary>
        /// Получить имена всех сотрудников в данном цехе
        /// </summary>
        /// <param name="query"></param>
        static void SearchNameWorkersInFactoryLinq(string query)
        {
            Console.WriteLine(" поиска имен рабочих заданого цеха");
            //Из коллекции заводов получить завод где название завода
            var search = from factory in Factory from list in factory.Value where factory.Key==query select list;
            foreach (var item in search)
                Console.WriteLine(item.Key.Name);
        }
        /// <summary>
        /// Получить имена всех сотрудников в данном цехе
        /// </summary>
        /// <param name="query"></param>
        static void SearchNameWorkersInFactory(string query)
        {
            Console.WriteLine("Поиск имен всех рабочих в данном цехе"+query);
            foreach (var item in Factory.SearchNameWorkers(query))
                Console.WriteLine(item.Key.Name);
        }

        #endregion


        #region Получение количества иженеров в данном цехе
        /// <summary>
        /// Получить количество инженеров в данном цехе
        /// </summary>
        /// <param name="query"></param>
        static void GetCountEngineersInFactory(string query)
        {
            Console.WriteLine("Получение количества инженеров в данном цехе"+query);
            Console.WriteLine("Количество инженеров в данном цехе "+Factory.EnginnersCounter(query));
        }
        /// <summary>
        /// Получить количество инженеров в данном цехе
        /// </summary>
        /// <param name="query"></param>
        static void GetCountEngineersInFactorylinq(string query)
        {
            Console.WriteLine("Получение количества инженеров в данном цехе");
            var search = from factory in Factory from list in factory.Value where factory.Key == query select list;
            int count = (from engineers in search where  engineers.Key is Engineer select engineers).Count();
            Console.WriteLine($"Количество инженеров в данном цехе {query} ={count}");
        }
        #endregion

        #region Агрегация
        /// <summary>
        /// Средний возраст всех рабочих в данном цехе
        /// </summary>
        /// <param name="query"></param>
        static void GetAgeAllWorkersInFactoryLinq(string query)
        {
            Console.WriteLine("Получить всех рабочих в данном цехе \n");
            var search = from factory in Factory from list in factory.Value where factory.Key == query select list;
            double averageAge = (from workers in search select workers).Average(worker=> worker.Key.Age);
            Console.WriteLine($"Средний возраст всех сотрудников в данном цехе {query} , = {Math.Round(averageAge,2)}");

        }
        
        /// <summary>
        /// Получить средний возраст всех рабочих в данном цехе
        /// </summary>
        /// <param name="query"></param>
        static void GetAgeAllWorkersInFactory(string query)
        {
            Console.WriteLine("Получить средний возраст всех сотрудников в данном цехе "+query+"\n");
            Console.WriteLine($"Средний возраст всех сотрудников в данном цехе {query} , = {Math.Round(Factory.AverageWorkersinSection(query))}");
        }


        #endregion

        #region  Операции над множествами
        /// <summary>
        /// операция над цехами в заводе
        /// </summary>
        static void OperationOnCollectionLinq(string f,string s)
        {
            Console.WriteLine($"Объеденение двух цехов 1:{f} 2:{s} -------------------------------------------\n");
            var first = from factory in Factory from list in factory.Value where factory.Key ==f select list;
            var second= from factory in Factory from list in factory.Value where factory.Key == s select list;
            var result = first.Concat(second);
            foreach (var item in result)
                Console.WriteLine(item.Key.aboutPerson());
            Console.WriteLine("Общее число рабочих стало равно"+result.Count().ToString());
        }
        /// <summary>
        /// Вывести рабочих в одинаковым возрастом в двух цехах
        /// </summary>
        /// <param name="f"></param>
        /// <param name="s"></param>
        static void OperationOnCollection(string f, string s)
        {
            Console.WriteLine($"Объеденение двух цехов 1:{f} 2:{s} \n\n");

            foreach (var item in Factory.SplitFactorySections(f, s))
                Console.WriteLine(item.Key.aboutPerson());
        }

        static void RemoveWorkersLinq(int age ,string f, string s)
        {
            Console.WriteLine("Вывести всех работников с заданым возрастом в двух цехах");
            var first = from factory in Factory from list in factory.Value where factory.Key == f where list.Key.Age==age select list;
            var second = from factory in Factory from list in factory.Value where factory.Key == s where list.Key.Age == age select list;
            var result = first.Concat(second);
            foreach (var item in result)
            {
                Console.WriteLine(item.Key.aboutPerson());
            }
        }

        static void RemoveWorkers(int age, string f, string s)
        {
            foreach (var item in Factory.GetWorkersWithAge(f,s,age))
            {
                Console.WriteLine(item.Key.aboutPerson());
            }
        }
        #endregion
        static void Print(MyDictionary<string, MyDictionary<Person, string>> collection)
        {
            foreach (var factory in collection)
            {
                Console.WriteLine($"Название цеха :{factory.Key}  Количество объектов {factory.Value.Count}");
                Console.WriteLine("-------------------------------------------------------------------------------------");
                foreach (var item in factory.Value)
                    Console.WriteLine(item.Value);
                Console.WriteLine("-------------------------------------------------------------------------------------");
            }
        }
        /// <summary>
        /// Генератор рандомной коллекции
        /// </summary>
        /// <returns></returns>
        static MyDictionary<Person,string> Generate()
        {
             MyDictionary<Person,string> collection = new MyDictionary<Person, string>(2);

            for (int i = 0; i <collection.Capacity ; i++)
            {
                int next = random.Next(5);
                if (next == 1)
                {
                    Person person = new Person(Path.GetRandomFileName(), random.Next(1, 75), Path.GetRandomFileName());
                    collection.Add(person, person.aboutPerson());
                }
                if (next== 2)
                {
                    Employee employee = new Employee(Path.GetRandomFileName(), random.Next(1, 75),
                        Path.GetRandomFileName(), random.Next(1, 75), Path.GetRandomFileName());
                    collection.Add(employee, employee.aboutPerson());
                }
                if (next == 3)
                {
                    Engineer engineer = new Engineer(Path.GetRandomFileName(), random.Next(1, 75),
                        Path.GetRandomFileName(), random.Next(1, 75), Path.GetRandomFileName(), random.Next(1, 7));
                    collection.Add(engineer, engineer.aboutPerson());
                }
                 if (next == 4)
                {
                    Workman workman = new Workman(Path.GetRandomFileName(), random.Next(1, 75),
                        Path.GetRandomFileName(), random.Next(1, 7500), Path.GetRandomFileName(),
                        Path.GetRandomFileName());
                    
                    collection.Add(workman, workman.aboutPerson());
                }
            }
            return collection;
        }
    }
}
