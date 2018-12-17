using System.Security.Claims;

namespace <%= solutionName %>.Core.Services.User
{
    public interface ICurrentUserProvider
    {
        string PhoneHashId(ClaimsPrincipal user);
    }
}
