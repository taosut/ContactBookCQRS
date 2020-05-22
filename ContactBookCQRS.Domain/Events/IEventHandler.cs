using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Events
{
    public interface IEventHandler
    {
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
