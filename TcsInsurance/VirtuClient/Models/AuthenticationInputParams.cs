namespace VirtuClient.Models
{
    public class AuthenticationInputParams
    {
        public string userName { get; set; }
        public string password { get; set; }
        public bool createPersistentCookie { get; set; }
    }
}
