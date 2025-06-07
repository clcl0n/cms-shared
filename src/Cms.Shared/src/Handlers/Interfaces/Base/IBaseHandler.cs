using System.Threading;
using System.Threading.Tasks;

namespace Cms.Shared.Handlers.Interfaces.Base;

public interface IBaseHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}

public interface IBaseHandler<TRequest>
{
    Task HandleAsync(TRequest request, CancellationToken cancellationToken);
}
