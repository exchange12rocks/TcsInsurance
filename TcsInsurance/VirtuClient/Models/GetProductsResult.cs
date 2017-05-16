using RestSharp;
namespace VirtuClient.Models
{

    public class VirtuRootResult<T>
    {
        public T d { get; set; }
    }
    public class VirtuResultArray<T>
    {
        public T[] Result { get; set; }
        public bool IsValid { get; set; }
        public object[] Errors { get; set; }
    }
    public class VirtuResult<T>
    {
        public T Result { get; set; }
        public bool IsValid { get; set; }
        public object[] Errors { get; set; }
    }

    public class GetProductResult
    {
        public string ID { get; set; }
        public string ObjectName { get; set; }
        public object ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object CODE { get; set; }
        public string bankLogo { get; set; }
    }

}