using RamSoft.Application.Contracts.Base;
using System.Security.Claims;

namespace RamSoft.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(/*IHttpContextAccessor httpContextAccessor*/)
        {
            UserId = "Test";// httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
