using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobApplicationManagement.Filters
{
    public class AuthorizationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                string? accountId = filterContext.HttpContext.Session.GetString("currentId");
                if (accountId == null)
                {
                    filterContext.Result = new RedirectToActionResult("Index", "Auth", null);
                    return;
                }
            }
        }

    }
}
