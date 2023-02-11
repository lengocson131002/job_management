using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories;

namespace JobApplicationManagement.Controllers
{
    public class CompanyController : Controller
    {

        private IBaseRepository<Company> _companyRepository;
        
        public CompanyController(IBaseRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IActionResult Index()
        {
            List<Company> companies = _companyRepository.GetAll().ToList();
            return View(companies);
        }
    }
}
