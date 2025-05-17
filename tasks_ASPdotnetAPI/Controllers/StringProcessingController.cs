using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using tasks_ASPdotnetAPI.Settings;
using tasks_csProject.tasks_consoleApp.RandomNum;
using tasks_csProject.tasks_consoleApp.StringWorker;


namespace tasks_ASPdotnetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Располагаем по пути "api/название_класса", но без суфикса "Controller"
    public class StringProcessingController : Controller
    {
        private readonly StringHandler _stringHandler = new StringHandler();
        private readonly string _randomApi;
        private readonly List<string> _blackList;

        public StringProcessingController(
            IOptions<RandomApiSettings> randomApiSettings,
            IOptions<BlackListSettings> blackListSettings)
        {
            _randomApi = randomApiSettings.Value.RandomApi;
            _blackList = blackListSettings.Value.BlackList;
        }

        /// <summary>
        /// Обработка входной строки, сортировка, 
        /// поиск последовательности с гласными, 
        /// счёт повторов и удаление случайного символа
        /// </summary>
        /// <param name="input">Строка со строчными буквами англ. алфавита</param>
        /// <param name="sortType">Тип сортировки. 0 - Быстрая сортировка. 1 - Сортировка деревом. </param>
        /// <returns>JSON с результатами обработки</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Успешный ответ
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Строка некорректна
        public async Task<IActionResult> ProcessString([FromQuery] string input, [FromQuery] SortType sortType)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return BadRequest("Строка не может быть пустой!");
            }

            if (!_stringHandler.CheckString(input, out var errorChars))
            {
                return BadRequest($"Некорректные символы: {string.Join(", ", errorChars)}. Допустимы только символы англ. алфавита в нижнем регистре");
            }

            if (_blackList.Any(word => input.Contains(word)))
            {
                return BadRequest($"Строка содержит запрещённые слова: {string.Join(", ", _blackList.Where(input.Contains))}");
            }

            // Обработка строки
            string processed = _stringHandler.ProcessString(input);
            string sorted = StringSorter.SortByEnumType(processed, sortType);
            string vowels = _stringHandler.FindLongestVowelSubstring(processed);
            var charRepeats = _stringHandler.GetNumOfDuplicateChar(processed)
                .ToDictionary(x =>  x.Key, x => x.Value);
            string removed = await RemoveRandomLetter(processed);

            var result = new
            {
                ProcessedString = processed,
                SortedString = sorted,
                VowelSubstring = string.IsNullOrEmpty(vowels) ? "Гласных подстрок не найдено" : vowels,
                Duplicates = charRepeats,
                ReducedString = removed
            };

            return Ok(result);
        }

        private async Task<string> RemoveRandomLetter(string str)
        {
            int index = await RandomNumProvider.GetIntAsync(0, str.Length - 1, _randomApi);
            return str.Remove(index, 1);
        }
    }
}
