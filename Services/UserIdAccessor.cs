using Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public class UserIdAccessor : IUserIdAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
