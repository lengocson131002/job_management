using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;

namespace JobApplicationManagement.Controllers
{
    public class JobDescriptionController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
