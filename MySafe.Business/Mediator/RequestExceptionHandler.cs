using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace MySafe.Business.Mediator.Pipelines
{
    public class RequestExceptionHandler<TRequest, TResponse>: IRequestExceptionHandler<TRequest, TResponse>
    {
        public Task Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state,
            CancellationToken cancellationToken)
        {
            throw new Exception();
        }
    }
}
