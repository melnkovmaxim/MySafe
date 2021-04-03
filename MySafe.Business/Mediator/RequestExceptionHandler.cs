using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using MySafe.Core.Entities.Abstractions;

namespace MySafe.Services.Mediator
{
    public class RequestExceptionHandler<TRequest, TResponse> : IRequestExceptionHandler<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IEntity
    {
        public Task Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}