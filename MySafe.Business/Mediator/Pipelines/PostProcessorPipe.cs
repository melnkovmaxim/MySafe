using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;

namespace MySafe.Business.Mediator.Pipelines
{
    public class PostProcessorPipe<TRequest, TResponse>: RequestPostProcessorBehavior<TRequest, TResponse>, IPipelineBehavior<TRequest, TResponse> 
        where TRequest: IRequest<TResponse>
    {
        public PostProcessorPipe(IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors) 
            : base(postProcessors)
        {
        }
    }
}
