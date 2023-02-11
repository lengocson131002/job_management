using Microsoft.AspNetCore.Mvc;

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
