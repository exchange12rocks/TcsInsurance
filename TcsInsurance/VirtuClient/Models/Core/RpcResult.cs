namespace VirtuClient.Models.Core
{
    public class RpcResult<T>
    {
        public string type { get; set; }
        public int tid { get; set; }
        public string action { get; set; }
        public string method { get; set; }
        public RpcDataResult<T> result { get; set; }
    }
    public class RpcDataResult<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T[] data { get; set; }
        public int totalCount { get; set; }
    }
}