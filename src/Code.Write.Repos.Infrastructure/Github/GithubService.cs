using Code.Writer.Repos.Domain.Contracts.Infrastructure.GitHub;
using Code.Writer.Repos.Domain.Github.Auth;
using Code.Writer.Repos.Domain.Settings;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Code.Writer.Repos.Infrastructure.Github
{
    public class GithubService : IGithubService
    {
        private readonly GithubSettings _githubSettings;
        private readonly DefaultContractResolver _contractJson;
        public GithubService(IOptions<GithubSettings> options)
        {
            _githubSettings = options.Value;
            _contractJson = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
        }
        public string GetAuthorizeUrl()
        {
            return "https://github.com/login/oauth/authorize"
                .SetQueryParam("client_id", _githubSettings.ClientId)
                .SetQueryParam("redirect_uri", _githubSettings.RedirectUri)
                .SetQueryParam("login", "")
                .SetQueryParam("scope", _githubSettings.Scope)
                .SetQueryParam("state", "")
                .SetQueryParam("allow_signup", _githubSettings.AllowSignup)
                .ToString(true);
        }
        public async Task<TokenResult> GetAccessToken(RequestTokenByAccessCode request)
        {
            var result = await "https://github.com/login/oauth/access_token"
                     .WithHeader("accept", "application/json")
                     .SetQueryParam("client_id", _githubSettings.ClientId)
                     .SetQueryParam("client_secret", _githubSettings.ClientSecret)
                     .SetQueryParam("code", request.Code)
                     .SetQueryParam("redirect_uri", _githubSettings.RedirectUri)
                     .PostAsync();
            if (result.StatusCode == 200)
            {
                var strResult = await result.ResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TokenResult>(strResult, new JsonSerializerSettings
                {
                    ContractResolver = _contractJson
                });
            }
            return null;
        }
    }
}
