using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hierarhy;

namespace Lab15
{
  

    public static class ObjectExtension
    {
        public static IEnumerable<KeyValuePair<Person,string>> SearchNameWorkers(this MyDictionary<string, MyDictionary<Person, string>> Factory,string query)
        {
            var search = from factory in Factory from list in factory.Value where factory.Key == query select list;
            return search;
        }

        public static int EnginnersCounter(this MyDictionary<string, MyDictionary<Person, string>> Factory,
            string query)
        {
            var search = from factory in Factory from list in factory.Value where factory.Key == query select list;
            return (from engineers in search where engineers.Key is Engineer select engineers).Count();
        }

        public static double AverageWorkersinSection(this MyDictionary<string, MyDictionary<Person, string>> Factory,
            string query)
        {
            var search = from factory in Factory from list in factory.Value where factory.Key == query select list;
            return (from workers in search select workers).Average(worker => worker.Key.Age);
        }

        public static IEnumerable<KeyValuePair<Person, string>> SplitFactorySections(this MyDictionary<string, MyDictionary<Person, string>> Factory,
            string f,string s)
        {
            var first = from factory in Factory from list in factory.Value where factory.Key == f select list;
            var second = from factory in Factory from list in factory.Value where factory.Key == s select list;
           return first.Concat(second);
        }

        public static IEnumerable<KeyValuePair<Person, string>> GetWorkersWithAge(
            this MyDictionary<string, MyDictionary<Person, string>> Factory,
            string f, string s, int age)
        {
            var first = from factory in Factory
                from list in factory.Value
                where factory.Key == f
                where list.Key.Age <age
                select list;

            var second = from factory in Factory
                from list in factory.Value
                where factory.Key == s
                where list.Key.Age < age
                select list;
            return first.Concat(second);
        }


    }

}

