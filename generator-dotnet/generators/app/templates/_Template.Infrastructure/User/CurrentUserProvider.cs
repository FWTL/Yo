using System.Linq;
using System.Security.Claims;
using <%= solutionName %>.Core.Services.User;

namespace <%= solutionName %>.Infrastructure.User
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public string PhoneHashId(ClaimsPrincipal user)
        {
            return user.Claims.First(c => c.Type == "PhoneHashId").Value;
        }
    }
}
