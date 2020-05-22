using System.Threading.Tasks;
using ContactBookCQRS.Domain.CommandHandlers;
using ContactBookCQRS.Domain.Commands;
using ContactBookCQRS.Domain.Events;
using MediatR;

namespace ContactBookCQRS.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : ICommandHandler, IEventHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStoreService _eventStore;

        public InMemoryBus(IEventStoreService eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore?.StoreEvent(@event);

            return _mediator.Publish(@event);
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
