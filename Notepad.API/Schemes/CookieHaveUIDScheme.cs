using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Notepad.API.Schemes
{
    public class CookieHaveUIDScheme : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ILoggerFactory _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public CookieHaveUIDScheme(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor contextAccessor) : base(options, logger, encoder, clock)
        {
            this._logger = logger;
            this._contextAccessor = contextAccessor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            await Task.CompletedTask;
            var cookie = _contextAccessor?.HttpContext?.Request.Cookies["UID"];
            if (cookie == null || string.IsNullOrWhiteSpace(cookie))
                return AuthenticateResult.Fail("You dont authorize");

            var strId = cookie.ToString();
            if(!Guid.TryParse(strId, out var id))
            {
                return AuthenticateResult.Fail("You have problem with your User NoteId");
            }
            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(GenerateClaims(Context), nameof(CookieHaveUIDScheme))), nameof(CookieHaveUIDScheme)));;
        }
        private IEnumerable<Claim> GenerateClaims(HttpContext context)
        {
            yield return new Claim("NoteId", context?.Request?.Cookies["UID"].ToString());
        }
    }
}
