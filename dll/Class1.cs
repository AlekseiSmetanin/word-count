using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Библиотека
{
    public class Class1
    {
        /// <summary>
        /// Разделяет текст на отдельные слова
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <returns>Список отдельных слов, содержащихся в тексте</returns>
        private static List<string> SplitToWords(string text)
        {
            text = text.ToLower();
            char[] separatorsArray = new char[] { ' ', ',', '.', '?', '!', '\r', '\n', '-', '[', ']', '(', ')' };
            string[] words = text.Split(separatorsArray, StringSplitOptions.RemoveEmptyEntries);

            return new List<string>(words);
        }

        /// <summary>
        /// Находит частоты встречаемости слов, отсортированные по убыванию
        /// </summary>
        /// <param name="words">Список, содержащий слова</param>
        /// <returns>Массив, отсортированный по убыванию, содержащий пары, состоящие из слова и частоты его встречаемости</returns>
        private static Dictionary<string, int> GetWordsFrequenciesArray(List<string> words)
        {
            Dictionary<string, int> wordsFrequencies = new Dictionary<string, int>();
            for (int i = 0; i < words.Count; i++)
            {
                if (!wordsFrequencies.ContainsKey(words[i]))
                    wordsFrequencies[words[i]] = 1;
                else
                    wordsFrequencies[words[i]]++;
            }

            //var wordsFrequenciesArray = wordsFrequencies.ToArray();
            //Array.Sort(wordsFrequenciesArray, (kvp1, kvp2) => -kvp1.Value.CompareTo(kvp2.Value));

            return wordsFrequencies;
        }

        /// <summary>
        /// Находит частоты встречаемости слов, отсортированные по убыванию
        /// </summary>
        /// <param name="text">Анализируемый текст</param>
        /// <returns>Массив, отсортированный по убыванию, содержащий пары, состоящие из слова и частоты его встречаемости</returns>
        private static Dictionary<string, int> GetWordsFrequencies(string text)
        {
            List<string> words = SplitToWords(text);

            return GetWordsFrequenciesArray(words);
        }
    }
}
