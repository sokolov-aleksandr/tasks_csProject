using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tasks_csProject.StringWorker
{
    /// <summary>
    /// Обработчик строк по различным правилам
    /// </summary>
    public class StringHandler
    {
        public StringHandler() { }

        /// <summary>
        /// Проверка строки на корректность, согласно условиям.
        /// Все символы должны быть на английском и содержать только буквы в нижнем регистре.
        /// </summary>
        /// <param name="str">Строка для проверки</param>
        /// <param name="errorChars">Список ошибочных символов, выявленных при проверке</param>
        /// <returns>Результат проверки строки: правильная или нет</returns>
        public bool CheckString(string str, out List<char> errorChars)
        {
            errorChars = new List<char>();
            bool isCorrect = true;

            foreach (char c in str)
            {
                // Проверка: если символ не в диапазоне a-z
                if (c < 'a' || c > 'z')
                {
                    if (!errorChars.Contains(c))
                    {
                        errorChars.Add(c);
                    }
                }
            }

            if (errorChars.Count > 0)
            {
                isCorrect = false;
            }

            return isCorrect;
        }
        
        /// <summary>
        /// Обработка строки по правилу чётной и нечётной длинны
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns>Обработанная по правилам строка</returns>
        public string ProcessString(string str)
        {
            string result = "";
            
            // Если чётная длинна
            if (str.Length % 2 == 0)
            {
                int half = str.Length / 2;
                string firstHalf = ReverseString(str.Substring(0, half));
                string secondHalf = ReverseString(str.Substring(half));
                result = firstHalf + secondHalf;
            }
            else // Если НЕчётная длинна
            {
                result = ReverseString(str) + str;
            }

            return result;
        }

        /// <summary>
        /// Получение количества повторений символа в строке
        /// </summary>
        /// <param name="str">Строка для нахождения количества</param>
        /// <returns>Словарь, где ключ - символ, а значение - его количество повторений в строке</returns>
        public Dictionary<char, int> GetNumOfDuplicateChar(string str)
        {
            Dictionary<char, int> keyValuePairs = new Dictionary<char, int>();

            foreach (char c in str)
            {
                if (keyValuePairs.ContainsKey(c))
                {
                    keyValuePairs[c] += 1;
                }
                else
                {
                    keyValuePairs.Add(c, 1);
                }
            }

            return keyValuePairs;
        }

        /// <summary>
        /// Переворачивание строки
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns>Перевёрнутая строка</returns>
        private string ReverseString(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
