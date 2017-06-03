namespace VirtuClient.Models
{
    public class GetPrintformsOutput
    {
        public string Key { get; set; }
        public GetPrintformsOutputValue Value { get; set; }
    }
    public class GetPrintformsOutputValue
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Transform { get; set; }
    }
}