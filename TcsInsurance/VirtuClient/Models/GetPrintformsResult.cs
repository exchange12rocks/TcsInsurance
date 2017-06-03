namespace VirtuClient.Models
{
    public class GetPrintformsResult
    {
        public string Key { get; set; }
        public GetPrintformsOutputValue Value { get; set; }
    }
    public class GetPrintformsResultValue
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Transform { get; set; }
    }
}