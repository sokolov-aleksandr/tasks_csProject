using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasks_csProject.StringWorker;

namespace tasks_csProject.App
{
    /// <summary>
    /// Консольный вариант приложения
    /// </summary>
    public class ConsoleApp : Application
    {
        private readonly StringHandler _stringHandler = new StringHandler();

        /// <summary>
        /// Логика всего приложения
        /// </summary>
        public override void RunApp()
        {
            ShowWelcomeMessage();

            while (true)
            {
                Console.Write("\nВведите строку для обработки: ");
                string str = Console.ReadLine();

                if (ShoudExit(str))
                {
                    Console.WriteLine("\nВыход из программы...");
                    break;
                }

                if (!ValidateInput(str))
                {
                    continue;
                }

                ShowResult(str);
            }
        }

        private void ShowWelcomeMessage()
        {
            Console.WriteLine("Добро пожаловать в обработчик строк");
            Console.WriteLine("Обрабатываются ТОЛЬКО строки с Английскими символами в НИЖНЕМ регистре!");
            Console.WriteLine("Для выхода введите \"exit\"\n");
        }

        private bool ShoudExit(string input)
        {
            return input.Trim().ToLower() == "exit";
        }

        /// <summary>
        /// Проверка правильности ввода строки пользователем
        /// </summary>
        /// <param name="str">Введённая пользователем строка</param>
        /// <returns>Результат проверки</returns>
        private bool ValidateInput(string str)
        {
            List<char> errorChars = new List<char>();
            
            if (string.IsNullOrWhiteSpace(str))
            {
                Console.WriteLine("Строка не может быть пустой!");
                return false;
            }

            if (!_stringHandler.CheckString(str, out errorChars))
            {
                Console.WriteLine($"В вводе содержатся ошибки: некорректны эти символы: {string.Join(" ", errorChars)}");
                Console.WriteLine("Обрабатываются ТОЛЬКО строки с Английскими символами в НИЖНЕМ регистре!");
                return false;
            }

            return true;
        }

        private void ShowResult(string str)
        {
            var processStr = _stringHandler.ProcessString(str);

            var titleMetod = (str.Length % 2 == 0)
                ? "Строка чётной длины: переворачиваем половинки."
                : "Строка НЕчётной длины: переворачиваем строку и добавляем её к оригиналу.";

            Console.WriteLine($"\n{titleMetod}" +
                $"\nОбработанная строка: {processStr}\n");
        }
    }
}
