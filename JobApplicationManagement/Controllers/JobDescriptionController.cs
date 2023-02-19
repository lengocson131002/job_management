using JobApplicationManagement.Filters;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class JobDescriptionController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
