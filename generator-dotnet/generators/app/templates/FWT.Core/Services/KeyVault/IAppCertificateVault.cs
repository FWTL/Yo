using System.Threading.Tasks;

namespace <%= solutionName %>.Core.Services.KeyVault
{
    public interface IAppCertificateVault
    {
        Task CreateSelfSignedAsync();
    }
}
