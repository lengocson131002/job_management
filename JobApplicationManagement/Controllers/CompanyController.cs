using JobApplicationManagement.Filters;
using JobApplicationManagement.Models.Company;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories;
using Repository.Repositories.interfaces;
using System.Diagnostics;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class CompanyController : Controller
    {
        private ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IActionResult Index()
        {
            List<Company> companies = _companyRepository.GetAll().ToList();
            return View(companies);
        }

        [HttpGet]
        public IActionResult CompanyDetail(long id)
        {
            Company company = _companyRepository.GetById(id);
            if (company == null)
            {
                return RedirectToAction(nameof(Index));
            }
            UpdateCompanyModel model = new UpdateCompanyModel()
            {
                Id = company.Id,
                Name = company.Name,
                Email = company.Email,
                Phone = company.Phone,
                Address = company.Address,
                Country = company.Country,
                Longitude = 0,
                Latitude = 0,
                MinScale = company.MinScale,
                MaxScale = company.MaxScale,
                Description = company.Description,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateCompany(UpdateCompanyModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateCompany", model);
            }
            var company = _companyRepository.GetById(model.Id);
            if (company == null)
            {
                TempData["Error"] = "Company Name not found!";
                return RedirectToAction(nameof(UpdateCompany));
            }

            company.Name = model.Name;
            company.Email = model.Email;
            company.Phone = model.Phone;
            company.Address = model.Address;
            company.Country = model.Country;
            company.Longitude = 0;
            company.Latitude = 0;
            company.MinScale = model.MinScale;
            company.MaxScale = model.MaxScale;
            company.Description = model.Description;

            _companyRepository.Update(company);
            _companyRepository.Save();
            TempData["Success"] = "Update company successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteCompany(long id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            Company company = _companyRepository.GetById(id);
            if (company != null)
            {
                _companyRepository.Delete(id);
                _companyRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            
            TempData["Success"] = "Delete company successfully!";

            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCompany(CreateCompanyModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCompany", model);
            }
            Company? company = _companyRepository.GetByName(model.Name);
            if (company != null)
            {
                TempData["Error"] = "Existed Company Name";
                return RedirectToAction(nameof(AddCompany));
            }
            Company? company1 = _companyRepository.GetByEmail(model.Email);
            if (company1 != null)
            {
                TempData["Error"] = "Existed Company Email";
                return RedirectToAction(nameof(AddCompany));
            }
            if (model.MinScale > model.MaxScale)
            {
                TempData["Error"] = "Max value must be greater than min value!";
                return RedirectToAction(nameof(AddCompany));

            }
            company = new Company()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                Country = model.Country,
                Longitude = 0,
                Latitude = 0,
                MinScale = model.MinScale,
                MaxScale = model.MaxScale,
                Description = model.Description,
            };

            _companyRepository.Insert(company);
            _companyRepository.Save();
            TempData["Success"] = "Create company successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public PartialViewResult GetCompany(GetAllCompanyModel model)
        {
            var companysPageResult = _companyRepository
               .GetAll(model.Query, model.PageNumber, model.PageSize);

            ViewData["PageNumber"] = model.PageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(companysPageResult.TotalItems * 1.0 / model.PageSize);

            List<Company> company = companysPageResult.Items;
            return PartialView("_CompanyListView", company);
        }


    }
}
