using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Common.Behaviours
{
    //ValidationBehaviour để xác thực các yêu cầu trước khi chúng được xử lý bởi các handler. Nó giúp đảm bảo rằng các yêu cầu gửi đến hệ thống đều hợp lệ
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<IRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<IRequest>> validator)
        {
            _validators = validator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any()) 
                return await next();
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if(failures.Any())
                throw new ValidationException(failures);
            return await next();
        }
    }
}
