using System.Net.Http;
using System.Text.Json;

namespace tasks_csProject.tasks_consoleApp.RandomNum
{
    public static class ApiRandomNumSource
    {
        /// <summary>
        /// Возвращает асинхронно рандомное int через API
        /// </summary>
        /// <param name="min">Минимальное возможное число</param>
        /// <param name="max">Максимальное возможное число</param>
        /// <returns>Рандомный int</returns>
        /// <exception cref="Exception">API вернул пустой ответ</exception>
        public static async Task<int> GetInt(int min, int max)
        {
            using HttpClient client = new HttpClient();
            string url = CreateUrlFromData(min, max);

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            int[]? result = JsonSerializer.Deserialize<int[]>(json);

            if (result == null || result.Length == 0)
                throw new Exception("Пустой ответ от API");

            return result[0];
        }

        private static string CreateUrlFromData(int min, int max)
        {
            string url = string.Empty;

            url += "http://www.randomnumberapi.com/api/v1.0/random?"
                + $"min={min}&max={max}&count=1";

            return url;
        }
    }
}
