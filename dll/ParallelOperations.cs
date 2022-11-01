using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Библиотека
{
    public class ParallelOperations
    {
        /// <summary>
        /// Находит частоты встречаемости слов
        /// </summary>
        /// <param name="text">Анализируемый текст</param>
        /// <returns>Словарь, содержащий слова и частоту их встречаемости</returns>
        private static Dictionary<string, int> GetWordsFrequencies(string text)
        {
            text = text.ToLower();
            char[] separatorsArray = new char[] { ' ', ',', '.', '?', '!', '\r', '\n', '-', '[', ']', '(', ')' };
            string[] words = text.Split(separatorsArray, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordsFrequencies = new Dictionary<string, int>();
            for (int i = 0; i < words.Length; i++)
            {
                if (!wordsFrequencies.ContainsKey(words[i]))
                    wordsFrequencies[words[i]] = 1;
                else
                    wordsFrequencies[words[i]]++;
            }

            return wordsFrequencies;
        }

        /// <summary>
        /// Разделяет строку на countOfParts примерно равных частей так, чтобы слова в строке не были разорваны (все разделения происходят по символам пробела)
        /// </summary>
        /// <param name="str">Разделяемая строка</param>
        /// <param name="countOfParts">Количество частей, на которые разделяется строка</param>
        /// <returns>Массив частей, на которые разделена строка</returns>
        private static string[] SplitToParts(string str, int countOfParts)
        {
            string[] parts = new string[countOfParts];
            List<int> positions = new List<int>(countOfParts);

            for (int i = 0; i < countOfParts; i++)
            {
                positions.Add(i * str.Length / countOfParts);

            }

            for (int i = 1; i < positions.Count; i++)
            {
                for (int j = positions[i]; j < str.Length; j++)
                {
                    if (str[positions[i]] != ' ')
                        positions[i]++;
                    else
                        break;
                }
            }

            positions.Add(str.Length);

            for (int i = 0; i < positions.Count - 1; i++)
            {
                parts[i] = str.Substring(positions[i], positions[i + 1] - positions[i]);
            }

            return parts;
        }

        /// <summary>
        /// Находит частоты встречаемости слов, отсортированные по убыванию
        /// </summary>
        /// <param name="text">Анализируемый текст</param>
        /// <returns>Словарь, содержащий пары, состоящие из слова и частоты его встречаемости</returns>
        public static Dictionary<string, int> GetWordsFrequenciesParallel(string text)
        {
            string[] partsOfText = SplitToParts(text, Environment.ProcessorCount);
            object locker = new object();

            Dictionary<string, int> wordsFrequencies = new Dictionary<string, int>();

            Parallel.ForEach(partsOfText, part =>
            {
                Dictionary<string, int> freqs = GetWordsFrequencies(part);

                lock (locker)
                {
                    foreach (var kvp in freqs)
                    {
                        if (!wordsFrequencies.ContainsKey(kvp.Key))
                        {
                            wordsFrequencies[kvp.Key] = kvp.Value;
                        }
                        else
                        {
                            wordsFrequencies[kvp.Key] += kvp.Value;
                        }
                    }
                }
            });

            return wordsFrequencies;
        }

    }
}
