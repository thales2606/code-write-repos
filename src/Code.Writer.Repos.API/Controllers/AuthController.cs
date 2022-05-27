using Code.Writer.Repos.Domain.Contracts.Infrastructure.GitHub;
using Code.Writer.Repos.Domain.Github.Auth;
using Code.Writer.Repos.Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Code.Writer.Repos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IGithubService _githubService;
        public AuthController(IGithubService githubService)
        {
            _githubService = githubService;
        }
        [HttpGet("githubAuthRedirect")]
        public IActionResult GithubAuthRedirect()
        {
            var urlRedirect = _githubService.GetAuthorizeUrl();
            return Redirect(urlRedirect);
        }
        [HttpGet("githubTokenResult")]
        public async Task<IActionResult> GithubTokenResult([FromQuery] RequestTokenByAccessCode request)
        {
           var result = await _githubService.GetAccessToken(request);

            return Ok(result);
        }

    }
}
