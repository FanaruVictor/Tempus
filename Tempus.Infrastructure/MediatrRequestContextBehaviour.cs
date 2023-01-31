using System.Diagnostics;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure;

public class MediatrRequestContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : BaseRequest<TResponse>
{
    private readonly IHttpContextAccessor _contextAccessor;

    public MediatrRequestContextBehaviour(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    [DebuggerStepThrough]
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var userIdClaim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User doesn't have the necessary claims");
            }
        
            request.UserId = Guid.Parse(userIdClaim.Value);

            var response = await next();
            return response;
        }

        return await next();
    }
}