using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Business.Mediator.Pipelines
{
    public class MediatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse: IResponse
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IEnumerable<IRequestPostProcessor<TRequest, TResponse>> _postProcessors;
        private readonly IEnumerable<BearerPreRequestHandler<TResponse>> _bearerPreProcessors;

        public MediatorPipeline(
            IEnumerable<IValidator<TRequest>> validators, 
            IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors,
            IEnumerable<BearerPreRequestHandler<TResponse>> bearerPreProcessors)
        {
            _validators = validators;
            _postProcessors = postProcessors;
            _bearerPreProcessors = bearerPreProcessors;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is BearerRequestBase<TResponse> bearerRequest)
            {
                foreach (var processor in _bearerPreProcessors)
                {
                    await processor.Process(bearerRequest, cancellationToken).ConfigureAwait(false);
                }
            }

            var context = new ValidationContext<TRequest>(request);
            var failures = _validators 
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {            
                var responseType = typeof(TResponse);
                var instance = Activator.CreateInstance(responseType) as IResponse;

                if (instance == null)
                {
                    var messages = failures.Select(x => x.ErrorMessage);
                    throw new ValidationException(string.Join(", ", messages));
                }

                instance.Error = failures.First().ErrorMessage;
                return (TResponse) instance;
            }

            var response = await next().ConfigureAwait(false);

            foreach (var processor in _postProcessors)
            {
                await processor.Process(request, response, cancellationToken).ConfigureAwait(false);
            }

            return response;
        }
    }
}
