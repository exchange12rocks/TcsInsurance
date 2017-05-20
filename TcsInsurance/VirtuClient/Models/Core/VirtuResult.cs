namespace VirtuClient.Models.Core
{
    public class VirtuResult<T> : IVirtuResult
    {
        public T Result { get; set; }
        public bool IsValid { get; set; }
        public string[] Errors { get; set; }
    }
}