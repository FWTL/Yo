using System.Threading.Tasks;

namespace <%= solutionName %>.Core.CQRS
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
