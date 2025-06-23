using Project1.Application.Contracts;
using System.Security.Claims;

namespace Project1.Presentation.Auth.CurrentUsers
{
    public class HttpContextCurrentUser(IHttpContextAccessor contextAccessor) : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public ClaimsPrincipal? Principal => _contextAccessor.HttpContext?.User;

        public int? UserId
        {
            get
            {
                var userId = Principal?.FindFirst("sub")?.Value;
                return userId != null ? int.Parse(userId) : null;
            }
        }

        public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated == true;
    }
}
