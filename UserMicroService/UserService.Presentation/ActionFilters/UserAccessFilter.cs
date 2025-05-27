using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserService.Presentation.ActionFilters
{
    public class UserAccessFilter : IAsyncActionFilter
    {
        private readonly IAccessControlService _accessControlService;

        public UserAccessFilter(IAccessControlService accessControlService)
        {
            _accessControlService = accessControlService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (!context.ActionArguments.TryGetValue("id", out var idObj) ||
                !(idObj is Guid id))
            {
                context.Result = new BadRequestObjectResult("ID is required");
                return;
            }

            var user = context.HttpContext.User;

            if (!await _accessControlService.CanAccessUserAsync(user, id))
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
