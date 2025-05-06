using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasks_csProject.StringWorker;

namespace tasks_csProject.App
{
    public class ConsoleApp : Application
    {
        private readonly StringHandler _stringHandler = new StringHandler();

        public override void RunApp()
        {
            Console.WriteLine("Добро пожаловать в обработчик строк");
            Console.WriteLine("Для выхода введите \"exit\"\n");

            while (true)
            {
                Console.Write("Введите строку для обработки: ");

                string str = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(str))
                {
                    Console.WriteLine("Строка не может быть пустой!");
                    continue;
                }
                if (str.Trim().ToLower() == "exit")
                {
                    Console.WriteLine("\nВыход из программы...");
                    break;
                }

                var processStr = _stringHandler.ProcessString(str);

                var titleMetod = (str.Length % 2 == 0)
                    ? "Строка чётной длины: переворачиваем половинки."
                    : "Строка НЕчётной длины: переворачиваем строку и добавляем её к оригиналу.";

                Console.WriteLine($"\n{titleMetod}" +
                    $"\nОбработанная строка: {processStr}\n");
            }
        }
    }
}
