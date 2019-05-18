using System.Collections;

namespace Infrastructure
{
    public interface IHandleCommand<TCommand>
    {
        IEnumerable Handle(TCommand command);
    }
}