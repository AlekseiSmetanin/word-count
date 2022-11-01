using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Клиент.ServiceReference1;

namespace Клиент
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client("BasicHttpBinding_IService1");
            try
            {
                Console.WriteLine(client.TestConnection());

                string text = File.ReadAllText("input.txt", Encoding.UTF8);
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Dictionary<string, int> wordsFrequencies = client.GetWordsFrequenciesParallel(text);
                WriteDictionaryToFile(wordsFrequencies, "output2.txt");
                stopwatch.Stop();
                Console.WriteLine("Многопоточный запуск {0} мс", stopwatch.ElapsedMilliseconds);

                stopwatch.Restart();
                wordsFrequencies =client.GetWordsFrequencies(text);
                WriteDictionaryToFile(wordsFrequencies, "output.txt");
                stopwatch.Stop();
                Console.WriteLine("Однопоточный запуск {0} мс", stopwatch.ElapsedMilliseconds);

                client.Close();

                Console.WriteLine("Работа программы завершена.\nВыходные данные записаны в файл output2.txt");
            }
            catch(Exception exc)
            {
                client.Abort();
                Console.WriteLine(exc);
            }

            Console.WriteLine("Для выхода нажмите Enter.");
            Console.ReadLine();
        }

        private static void WriteDictionaryToFile(Dictionary<string, int> wordsFrequencies, string path)
        {
            var wordsFrequenciesArray = wordsFrequencies.ToArray();
            Array.Sort(wordsFrequenciesArray, (kvp1, kvp2) => -kvp1.Value.CompareTo(kvp2.Value));

            StreamWriter streamWriter = new StreamWriter(path);
            for (int i = 0; i < wordsFrequenciesArray.Length; i++)
            {
                streamWriter.WriteLine("{0,-30}{1,10}", wordsFrequenciesArray[i].Key, wordsFrequenciesArray[i].Value);
            }
            streamWriter.Close();
        }
    }
}
