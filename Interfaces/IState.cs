using Pokemon_Go_Threaded_Trainer.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon_Go_Threaded_Trainer.States
{
    public interface IState
    {
        Task<IState> Execute(ISession session, CancellationToken cancellationToken);
    }
}
