using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Project1.Presentation.Auth.Handlers
{
    public class TokenAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
        ) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        private const string prefix = "My token ";

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            var schemePrefix = "Bearer ";

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith(schemePrefix))
            {
                return AuthenticateResult.NoResult();
            }

            var token = authHeader[schemePrefix.Length..].Trim();
            if (!await IsValidToken(token))
            {
                return AuthenticateResult.Fail("Invalid token");
            }

            var claims = await ExtractClaimsFromToken(token);
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        private static Task<bool> IsValidToken(string token)
        {
            // In practice, you would validate the token against a database or an external service.
            return Task.FromResult(token.StartsWith(prefix));
        }

        private static Task<IEnumerable<Claim>> ExtractClaimsFromToken(string token)
        {
            var sub = token[prefix.Length..].Trim();
            var claims = new[]
            {
                new Claim("sub", sub),
            };
            return Task.FromResult(claims.AsEnumerable());
        }
    }
}
