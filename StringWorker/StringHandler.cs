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
