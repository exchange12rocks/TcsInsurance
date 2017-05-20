namespace VirtuClient.Models.Core
{
    public class SimpleResult
    {
        public bool d { get; set; }
        public bool Success
        {
            get
            {
                return this.d;
            }
        }
        public void ThrowExceptionIfFail()
        {
            if(!this.Success)
            {
                throw new VirtuException();
            }
        }
    }
}
