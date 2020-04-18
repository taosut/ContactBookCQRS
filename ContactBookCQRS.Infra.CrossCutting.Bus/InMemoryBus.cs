using System.Threading.Tasks;
using ContactBookCQRS.Domain.Core.Bus;
using ContactBookCQRS.Domain.Core.Commands;
using MediatR;

namespace ContactBookCQRS.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
