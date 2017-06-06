namespace VirtuClient.Models.Core
{
    public class RpcInput<T>
    {
        public string action { get; set; }
        public string method { get; set; }
        public int tid { get; set; }
        public string type { get; set; }
        public T[] data { get; set; }
    }
}