namespace Outloud.Common.Authentication
{
    public class Auth0Options
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string Claim { get; set; }
        public bool RequireHttps { get; set; }
    }
}