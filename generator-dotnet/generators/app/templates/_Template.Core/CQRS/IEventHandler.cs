using System.Threading.Tasks;

namespace <%= solutionName %>.Core.CQRS
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task ExecuteAsync(TEvent @event);
    }
}
