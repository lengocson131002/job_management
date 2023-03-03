using JobApplicationManagement.Filters;
using JobApplicationManagement.Models.Contract;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories;
using Repository.Repositories.interfaces;
using System.ComponentModel.Design;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]

    public class ContractController : Controller
    {
        private readonly ILogger<ContractController> _logger;
        private readonly ContractRepository _contractRepository;
        private IBaseRepository<Company> _companyResposiotory;
        private ResumeRepository _resumeRepository;

        public ContractController(
            ILogger<ContractController> logger,
            ContractRepository contractRepository,
            IBaseRepository<Company> companyRepository,
            ResumeRepository resumeRepository
            )
        {
            _logger = logger;
            _contractRepository = contractRepository;
            _companyResposiotory = companyRepository;
            _resumeRepository = resumeRepository;
        }

        [HttpGet]
        public ActionResult Index(GetAllContractModel model)
        {
            var contractPageResult = _contractRepository.GetAll(model.CompanyId, model.PageNumber, model.PageSize);
            
            ViewData["TotalPages"] = (int)Math.Ceiling(contractPageResult.TotalItems * 1.0 / model.PageSize);
            ViewData["PageNumber"] = contractPageResult.PageNumber;

            return View(contractPageResult.Items);
        }

        [HttpGet]
        public ActionResult Create(long companyId, long resumeId)
        {
            var company = _companyResposiotory.GetById(companyId);
            var resume = _resumeRepository.GetById(resumeId);

            if (company == null || resume == null)
            {
                return NotFound();
            }

            var model = new CreateContractModel
            {
                CompanyId = company.Id,
                CompanyName = company.Name,
                ResumeId = resume.Id,
                ResumeName = resume.Name
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(CreateContractModel model)
        {
            DateTime now = DateTime.Now;
            if (DateTime.Compare(now, model.InterviewTime) > 0)
            {
                ModelState.AddModelError("InterviewTime", "The Interview time must be later than now");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var company = _companyResposiotory.GetById(model.CompanyId);
            var resume = _resumeRepository.GetById(model.ResumeId);

            if (company == null || resume == null)
            {
                return NotFound();
            }

            var contract = new Contract
            {
                Company = company,
                Resume = resume,
                InterviewTime = model.InterviewTime,
                Interviewer = model.Interviewer,
                OfferSalary = model.OfferSalary,
                RequestSalary = model.RequestSalary
            };

            _contractRepository.Insert(contract);
            return RedirectToAction(nameof(Index));
        }
    }
}
