using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSDemo.PipelineBehaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Request
            logger.LogInformation($"Handling {typeof(TRequest).Name}");
            foreach (var prop in request.GetType().GetProperties())
            {
                object propValue = prop.GetValue(request, null);
                logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
            }
            var response = await next();

            //Response
            logger.LogInformation($"Handled {typeof(TResponse).Name}");
            return response;
        }
    }
}
