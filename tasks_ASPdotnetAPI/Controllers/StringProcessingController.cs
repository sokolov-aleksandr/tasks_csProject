using Microsoft.AspNetCore.Mvc;
using tasks_csProject.tasks_consoleApp.RandomNum;
using tasks_csProject.tasks_consoleApp.StringWorker;


namespace tasks_ASPdotnetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Располагаем по пути "api/название_класса", но без суфикса "Controller"
    public class StringProcessingController : Controller
    {
        private readonly StringHandler _stringHandler = new StringHandler();

        [HttpGet]
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

            // Обработка строки
            string processed = _stringHandler.ProcessString(input);
            string sorted = StringSorter.SortByEnumType(input, sortType);
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
            int index = await RandomNumProvider.GetIntAsync(0, str.Length - 1);
            return str.Remove(index, 1);
        }
    }
}
