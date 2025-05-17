using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tasks_csProject.tasks_consoleApp.RandomNum
{
    /// <summary>
    /// Получение рандомного числа различными способами
    /// </summary>
    public static class RandomNumProvider
    {
        /// <summary>
        /// Получить асинхронно рандомное int (если возможно, то по API, а если нет, то через dotnet) 
        /// </summary>
        /// <param name="min">Минимальное возможное число</param>
        /// <param name="max">Максимальное возможное число</param>
        /// <returns>Рандомный int</returns>
        public static async Task<int> GetIntAsync(int min, int max)
        {
            try
            {
                // Получаем рандомное число из API
                return await ApiRandomNumSource.GetInt(min, max);
            }
            catch
            {
                // fallback
                return new Random().Next(min, max); // Получаем рандомное число из dotnet
            }
        }
    }
}
