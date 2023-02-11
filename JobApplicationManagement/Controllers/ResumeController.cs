using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
