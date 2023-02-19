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
        public IActionResult CompanyDetail(long id)
        {
            Company company = _companyRepository.GetById(id);
            return View(company);
        }
        public IActionResult AddCompany()
        {
            return View();
        }

        public IActionResult CreateCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCompany", company);
            }
            company.Longitude = 0;
            company.Latitude = 0;
            company.CreatedAt = DateTime.Now;
            _companyRepository.Insert(company);
            _companyRepository.Save();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateCompany(Company company)
        {
            company.Longitude = 0;
            company.Latitude = 0;
            company.ModifiedAt = DateTime.Now;
            _companyRepository.Update(company);
            _companyRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
