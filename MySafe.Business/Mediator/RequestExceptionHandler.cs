using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Business.Mediator.Pipelines
{
    public class RequestExceptionHandler<TRequest, TResponse>: IRequestExceptionHandler<TRequest, TResponse> 
        where TRequest   : IRequest
        where TResponse  : IResponse
    {
        public Task Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
