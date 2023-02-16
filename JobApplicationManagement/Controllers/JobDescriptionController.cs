using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;

namespace JobApplicationManagement.Controllers
{
    public class JobDescriptionController : Controller
    {
        private JobDescriptionRepository _jobDescriptionRepository;

        public JobDescriptionController(JobDescriptionRepository jobDescriptionRepository)
        {
            _jobDescriptionRepository = jobDescriptionRepository;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
