using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
