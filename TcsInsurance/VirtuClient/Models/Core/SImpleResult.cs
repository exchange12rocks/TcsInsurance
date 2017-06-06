namespace VirtuClient.Models.Core
{
    public class SimpleResult<T>
    {
        public T d { get; set; }
    }
    public class SimpleDataResult<T>
    {
        public T Result { get; set; }
        public bool IsValid { get; set; }
        public string[] Errors { get; set; }
    }
}