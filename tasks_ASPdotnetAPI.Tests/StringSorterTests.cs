/// <summary>
/// Проверка методов сортировки
/// </summary>
[TestFixture]
public class StringSorterTests
{
    /// <summary>
    /// Проверка сортировки всеми методамиы
    /// </summary>
    /// <param name="input">текст для сортировки</param>
    /// <param name="type">Тип сортировки</param>
    /// <returns></returns>
    [TestCase("dcba", SortType.QuickSort, ExpectedResult = "abcd")]
    [TestCase("dcba", SortType.TreeSort, ExpectedResult = "abcd")]
    public string SortByEnumType_ValidInputs_ReturnsSorted(string input, SortType type)
    {
        return StringSorter.SortByEnumType(input, type);
    }

    /// <summary>
    /// Если выбран ошибочный тип сортировки
    /// </summary>
    [Test]
    public void SortByEnumType_InvalidEnum_Throws()
    {
        // (SortType)999 не валидный
        Assert.Throws<ArgumentException>(() =>
            StringSorter.SortByEnumType("abc", (SortType)999));
    }
}
