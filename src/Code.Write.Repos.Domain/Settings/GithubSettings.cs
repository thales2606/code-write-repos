namespace Code.Writer.Repos.Domain.Settings
{
    public class GithubSettings
    {
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
        public string AllowSignup { get; set; }
        public string ClientSecret { get; set; }
    }
}
