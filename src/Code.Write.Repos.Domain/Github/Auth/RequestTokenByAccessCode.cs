namespace Code.Writer.Repos.Domain.Github.Auth
{
    public class RequestTokenByAccessCode
    {
        public string? Code { get;set; }
        public string? State { get; set; }
        public string? Error { get; set; }
        public string? ErrorDescription { get; set; }
        public string? ErrorUri { get; set; }
    }
}
