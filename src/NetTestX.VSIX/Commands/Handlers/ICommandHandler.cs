using System.Threading.Tasks;

namespace NetTestX.VSIX.Commands.Handlers;

public interface ICommandHandler
{
    Task ExecuteAsync();
}
