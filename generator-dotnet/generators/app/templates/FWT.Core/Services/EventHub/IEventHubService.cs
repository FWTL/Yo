using System.Collections.Generic;
using System.Threading.Tasks;

namespace <%= solutionName %>.Core.Services.EventHub
{
    public interface IEventHubService
    {
        Task SendAsync<TMessage>(List<TMessage> messages);
    }
}
