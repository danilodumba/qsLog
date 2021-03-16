using MediatR;

namespace qsLog.Applications.Core
{
    public abstract class Command<TResult> : IRequest<TResult>
    {
        public abstract bool IsValid();
    }
}