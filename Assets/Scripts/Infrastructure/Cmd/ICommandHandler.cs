using Infrastructure.Services;

namespace Infrastructure.Cmd
{
    public interface ICommandHandler<TCommand>:IService where TCommand:ICommand
    {
        bool TryHandle(TCommand command); 
    }
}
