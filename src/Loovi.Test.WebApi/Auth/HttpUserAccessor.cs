using Loovi.Test.Common.Auth.Interfaces;
using System.Security.Claims;

namespace Loovi.Test.WebApi.Auth
{
    public class HttpUserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public string? GetUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        }

        public string? GetRole()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        }
    }
}
