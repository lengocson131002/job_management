using JobApplicationManagement.Filters;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.interfaces;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
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
