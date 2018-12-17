using System.Threading.Tasks;
using OpenTl.ClientApi;

namespace <%= solutionName %>.Core.Services.Telegram
{
    public interface ITelegramService
    {
        Task<IClientApi> BuildAsync(string hash);
    }
}
