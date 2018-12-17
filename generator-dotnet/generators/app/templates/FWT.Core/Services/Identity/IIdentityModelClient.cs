using System.Threading.Tasks;
using IdentityModel.Client;
using OpenTl.Schema;

namespace <%= solutionName %>.Core.Services.Identity
{
    public interface IIdentityModelClient
    {
        Task<TokenResponse> RequestClientCredentialsTokenAsync(TUser user);
    }
}
