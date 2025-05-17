using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasks_csProject.tasks_consoleApp.RandomNum;
using tasks_csProject.tasks_consoleApp.StringWorker;

namespace tasks_csProject.tasks_consoleApp.App
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
        public override async Task RunApp()
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


                SortType sortType = SelectSortType();

                await ShowResult(str, sortType);
            }
        }

        private void ShowWelcomeMessage()
        {
            Console.WriteLine("Добро пожаловать в обработчик строк");
            Console.WriteLine("Обрабатываются ТОЛЬКО строки с Английскими символами в НИЖНЕМ регистре!");
            Console.WriteLine("Для выхода введите \"exit\"\n");
        }

        private SortType SelectSortType()
        {
            Console.WriteLine("\n Выберите способ сортировки строки:");
            Console.WriteLine("1 - Быстрая сортировка");
            Console.WriteLine("2 - Сортировка двоичным деревом (TreeSort)");

            while (true)
            {
                Console.WriteLine("Выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return SortType.QuickSort;
                    case "2":
                        return SortType.TreeSort;
                    default:
                        Console.WriteLine("Неверный выбор, введите заново!\n");
                        break;
                }
            }
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
            if (string.IsNullOrWhiteSpace(str))
            {
                Console.WriteLine("Строка не может быть пустой!");
                return false;
            }

            if (!_stringHandler.CheckString(str, out HashSet<char> errorChars))
            {
                Console.WriteLine($"В вводе содержатся ошибки: некорректны эти символы: {string.Join(" ", errorChars)}");
                Console.WriteLine("Обрабатываются ТОЛЬКО строки с Английскими символами в НИЖНЕМ регистре!");
                return false;
            }

            return true;
        }

        private async Task<string> RemoveRandomLetter(string str)
        {
            int index = await RandomNumProvider.GetIntAsync(0, str.Length - 1);

            string result = str.Remove(index, 1);

            Console.WriteLine($"\nИндекс удалённого символа: {index}");

            return result;
        }

        private async Task ShowResult(string str, SortType sortType)
        {
            Console.WriteLine();

            // Обрабатываем строку
            var processStr = _stringHandler.ProcessString(str);

            // Правила используемые при обработке
            var titleMetod = str.Length % 2 == 0
                ? "Строка чётной длины: переворачиваем половинки."
                : "Строка НЕчётной длины: переворачиваем строку и добавляем её к оригиналу.";

            // Вывод обработанной строки
            Console.WriteLine($"\n{titleMetod}" +
                $"\nОбработанная строка:    {processStr}");
            // Вывод ОТСОРТИРОВАННОЙ обработанной строки
            Console.WriteLine($"Отсортированная строка: {StringSorter.SortByEnumType(processStr, sortType)}\n");

            // Вывод подстроки с гласными
            var vowelSubStr = _stringHandler.FindLongestVowelSubstring(processStr);
            if (vowelSubStr == string.Empty)
            {
                Console.WriteLine("В обработанной строке нет гласных (или она одна), поэтому найди подстроку нельзя\n");
            }
            else
            {
                Console.WriteLine($"Самая длинная подстрока, которая начинается и заканчивается гласной в обработанной строке: " +
                    $"{vowelSubStr}\n");
            }

            // Вывод повторений символов в строке
            Console.WriteLine("Количество повторений символов в обработанной строке: ");
            foreach ((char chr, int count) in _stringHandler.GetNumOfDuplicateChar(processStr))
            {
                Console.WriteLine($"    {chr} - {count} раз");
            }

            string result = await RemoveRandomLetter(processStr);
            Console.WriteLine($"Строка без рандомного символа: {result}");
        }
    }
}
