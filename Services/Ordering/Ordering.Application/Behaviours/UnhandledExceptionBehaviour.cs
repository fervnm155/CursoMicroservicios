using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger logger;

        public UnhandledExceptionBehaviour(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse>Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch(Exception ex)
            {
                var requestName=typeof(TRequest).Name;
                logger.LogError(ex, $"Excepcion no controlada para la peticion {requestName}.");
                throw;
            }
        }
    }
}
