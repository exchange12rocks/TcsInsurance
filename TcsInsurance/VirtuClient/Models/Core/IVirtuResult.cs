namespace VirtuClient.Models.Core
{
    public interface IVirtuResult
    {
        bool IsValid { get; set; }
        string[] Errors { get; set; }
    }
}