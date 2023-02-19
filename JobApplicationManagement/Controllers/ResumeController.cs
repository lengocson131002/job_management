using JobApplicationManagement.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class ResumeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
