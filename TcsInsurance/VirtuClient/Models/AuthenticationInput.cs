namespace VirtuClient.Models
{
    public class AuthenticationInput
    {
        public string userName { get; set; }
        public string password { get; set; }
        public bool createPersistentCookie { get; set; }
    }
}