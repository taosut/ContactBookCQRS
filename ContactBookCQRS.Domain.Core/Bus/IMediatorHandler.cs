using ContactBookCQRS.Domain.Core.Commands;
using System.Threading.Tasks;

namespace ContactBookCQRS.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
    }
}
