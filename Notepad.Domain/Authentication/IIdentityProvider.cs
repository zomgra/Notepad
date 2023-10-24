using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Notepad.Domain.Authentication
{
    public interface IIdentityProvider
    {
        Guid UserId { get; }
    }
    public class IdentityProvider : IIdentityProvider
    {
        private IHttpContextAccessor _httpContextAccessor { get; set; }

        public IdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var strId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrWhiteSpace(strId))
                    return Guid.Empty;
                var id = Guid.Parse(strId);
                return id;
            }
        }
    }
}
