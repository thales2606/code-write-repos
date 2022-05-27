using Code.Writer.Repos.Domain.Github.Auth;

namespace Code.Writer.Repos.Domain.Contracts.Infrastructure.GitHub
{
    public interface IGithubService
    {
        string GetAuthorizeUrl();
        Task<TokenResult> GetAccessToken(RequestTokenByAccessCode request);
    }
}
