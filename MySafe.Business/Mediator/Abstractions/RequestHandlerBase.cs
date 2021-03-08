using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MySafe.Business.Mediator.Abstractions
{
    public class RequestHandlerBase<TRequest, TResponse>: IRequestHandler<TRequest, TResponse>
    where TRequest: IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
