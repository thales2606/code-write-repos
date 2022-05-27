namespace Code.Writer.Repos.Domain.Github.Auth
{
    public class TokenResult
    {
        public string AccessToken { get; set; }
        public string scope { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenExpiresIn { get; set; }
    }
}
