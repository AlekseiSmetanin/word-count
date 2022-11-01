using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Библиотека;


namespace Школа_инженера_ДЗ4
{
    public class Service1 : IService1
    {
        /// <summary>
        /// проверка соединения
        /// </summary>
        /// <returns> OK </returns>
        public string TestConnection()
        {
            return "OK";
        }

        public Dictionary<string, int> GetWordsFrequenciesParallel(string text)
        {
            return ParallelOperations.GetWordsFrequenciesParallel(text);
        }

        public Dictionary<string, int> GetWordsFrequencies(string text)
        {
            Assembly assembly = Assembly.LoadFile(@"C:\Users\user1\source\repos\Школа инженера ДЗ4\Библиотека\bin\Debug\Библиотека.dll");
            var type = assembly.GetTypes().First(x => x.Name == "Class1");
            var mi = type.GetMethod("GetWordsFrequencies", BindingFlags.NonPublic | BindingFlags.Static);

            return (Dictionary<string, int>)mi.Invoke(new object(), new object[] { text });
        }
    }
}
