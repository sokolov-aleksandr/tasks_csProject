using tasks_csProject.tasks_consoleApp.StringWorker;

/// <summary>
/// Проверка методов работы со строкой
/// </summary>
[TestFixture]
public class StringHandlerTests
{
    private StringHandler _handler;

    [SetUp] // Выполняется перед каждым тестом
    public void Setup() => _handler = new StringHandler();

    /// <summary>
    /// Строка только из латинских букв должна проходить проверку без ошибок
    /// </summary>
    [Test]
    public void CheckString_ValidLowercase_ReturnsTrueAndEmptyErrors()
    {
        string input = "abcxyz";

        bool ok = _handler.CheckString(input, out var errors);

        Assert.IsTrue(ok);
        Assert.IsEmpty(errors);
    }

    /// <summary>
    /// Строка с неправильными символами НЕ должна проходить проверку, а выводить ошибочные символы
    /// </summary>
    [Test]
    public void CheckString_InvalidChars_ReturnsFalseAndListsErrors()
    {
        string input = "abC1!";

        bool ok = _handler.CheckString(input, out var errors);

        Assert.IsFalse(ok);
        CollectionAssert.AreEquivalent(new[] { 'C', '1', '!' }, errors);
    }

    /// <summary>
    /// Проверка обработки строки: чётная длинна - переворачивание половинок
    /// </summary>
    [Test]
    public void ProcessString_EvenLength_ReversesEachHalf()
    {
        // "abcd" => "ab"=>"ba", "cd"=>"dc". Результат => "badc"
        Assert.AreEqual("badc", _handler.ProcessString("abcd"));
    }

    /// <summary>
    /// Проверка обработки строки: НЕчётная длинна - переворот строки и прибавление к ней исходной
    /// </summary>
    [Test]
    public void ProcessString_OddLength_ReversesWholeAndConcats()
    {
        // "abc" => "cba" + "abc" => "cbaabc"
        Assert.AreEqual("cbaabc", _handler.ProcessString("abc"));
    }

    /// <summary>
    /// Проверка поиска дупликатов символов и их счёта
    /// </summary>
    [Test]
    public void GetNumOfDuplicateChar_CountsCorrectly()
    {
        var result = _handler.GetNumOfDuplicateChar("aabbc");
        var expected = new Dictionary<char, int> { ['a'] = 2, ['b'] = 2, ['c'] = 1 };
        CollectionAssert.AreEquivalent(expected, result);
    }

    /// <summary>
    /// Нахождение самой длинной подстроки, которая начинается и закарнчивается на гласную
    /// </summary>
    [Test]
    public void FindLongestVowelSubstring_FindsCorrect()
    {
        // В "gsdafogred" есть подстрока "ogre"
        Assert.AreEqual("ogre", _handler.FindLongestVowelSubstring("gsdafogred"));
    }

    /// <summary>
    /// Гласных в строке нет, поэтому подстроки с гласными нет
    /// </summary>
    [Test]
    public void FindLongestVowelSubstring_NoVowels_ReturnsEmpty()
    {
        Assert.AreEqual(string.Empty, _handler.FindLongestVowelSubstring("bcdfg"));
    }
}


