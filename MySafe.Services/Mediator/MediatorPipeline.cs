using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using MySafe.Core.Entities.Abstractions;
using MySafe.Services.Mediator.Abstractions;

namespace MySafe.Services.Mediator
{
    public class MediatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IEntity
    {
        private readonly IEnumerable<BearerPreRequestHandler<TResponse>> _bearerPreProcessors;
        private readonly IEnumerable<IRequestPostProcessor<TRequest, TResponse>> _postProcessors;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public MediatorPipeline(
            IEnumerable<IValidator<TRequest>> validators,
            IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors,
            IEnumerable<BearerPreRequestHandler<TResponse>> bearerPreProcessors)
        {
            _validators = validators;
            _postProcessors = postProcessors;
            _bearerPreProcessors = bearerPreProcessors;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                if (request is BearerRequestBase<TResponse> bearerRequest)
                    foreach (var processor in _bearerPreProcessors)
                        await processor.Process(bearerRequest, cancellationToken).ConfigureAwait(false);

                var context = new ValidationContext<TRequest>(request);
                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var responseType = typeof(TResponse);
                    var instance = Activator.CreateInstance(responseType) as IEntity;

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
                    await processor.Process(request, response, cancellationToken).ConfigureAwait(false);

                return response;
            }
            catch (Exception ex)
            {
                //TODO логировать + добавить еще исключений

                var errorResponse = Activator.CreateInstance<TResponse>();
                errorResponse.Error = ex.Message;

                return errorResponse;
            }
        }
    }
}