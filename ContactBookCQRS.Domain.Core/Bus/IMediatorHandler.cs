using ContactBookCQRS.Domain.Core.Commands;
using ContactBookCQRS.Domain.Core.Events;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
