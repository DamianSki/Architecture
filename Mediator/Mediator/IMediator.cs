using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken token = default);
    }
}
