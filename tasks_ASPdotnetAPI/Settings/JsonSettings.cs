namespace tasks_ASPdotnetAPI.Settings
{
    public class JsonSettings
    {
        public List<string> BlackList { get; set; } = new();
        public int ParallelLimit { get; set; } = 5; // по умолчанию
    }
}
