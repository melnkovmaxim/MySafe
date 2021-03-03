using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Models.Responses.Abstractions;

namespace MySafe.Presentation.Mediator.Pipelines
{
    public class ValidationPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest: IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipe(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
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
                return Task.FromResult((TResponse) instance);
            }

            return next();
        }
    }
}
