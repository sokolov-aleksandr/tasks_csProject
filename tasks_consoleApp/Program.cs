﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasks_csProject.tasks_consoleApp.App;

namespace tasks_csProject.tasks_consoleApp
{
    internal class Program
    {
        // Запускаем консольный вариант приложения
        private static ConsoleApp _app = new ConsoleApp();

        static async Task Main(string[] args)
        {
            await _app.RunApp();
        }
    }
}

