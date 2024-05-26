using System.Threading.Tasks;

namespace NetTestX.VSIX.Commands.Handlers;

/// <summary>
/// Represents a handler capable of executing commands
/// </summary>
public interface ICommandHandler
{
    /// <summary>
    /// Execute this command asynchronously
    /// </summary>
    Task ExecuteAsync();
}
