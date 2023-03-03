
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobApplicationManagement.Extends
{
    public static class ViewContextExtends
    {
        public static string MakeActiveClass(this ViewContext viewContext, string controller, string className)
        {
            string? controllerName = viewContext.RouteData.Values["controller"]?.ToString();

            if (controllerName == null || !controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                return "";
            }

            return className;
        }
    }
}
