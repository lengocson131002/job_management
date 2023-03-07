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
        private readonly JobDescriptionRepository _jobDescriptionRepository;
        private readonly IBaseRepository<Company> _companyResposiotory;
        private readonly ResumeRepository _resumeRepository;

        public ContractController(
            ILogger<ContractController> logger,
            ContractRepository contractRepository,
            IBaseRepository<Company> companyRepository,
            JobDescriptionRepository jobDescriptionRepository,
            ResumeRepository resumeRepository
            )
        {
            _logger = logger;
            _contractRepository = contractRepository;
            _companyResposiotory = companyRepository;
            _resumeRepository = resumeRepository;
            _jobDescriptionRepository = jobDescriptionRepository;
        }

        [HttpGet]
        public ActionResult Index(GetAllContractModel model)
        {
            var contractPageResult = _contractRepository.GetAll(model.CompanyId, model.PageNumber, model.PageSize);
            
            ViewData["TotalPages"] = (int)Math.Ceiling(contractPageResult.TotalItems * 1.0 / model.PageSize);
            ViewData["PageNumber"] = contractPageResult.PageNumber;
            ViewData["Companies"] = _companyResposiotory.GetAll();
            ViewData["SelectedCompanyId"] = model.CompanyId;

            return View(contractPageResult.Items);
        }

        [HttpGet]
        public ActionResult Create(long companyId, long resumeId, long jobDescriptionId)
        {
            if (_contractRepository.FindByCompanyIdAndResumeId(companyId, resumeId) != null)
            {
                TempData["Error"] = "Resume has been had contract with this company!";
                return RedirectToAction(nameof(Index));
            }

            var company = _companyResposiotory.GetById(companyId);
            var resume = _resumeRepository.GetById(resumeId);
            var job = _jobDescriptionRepository.GetById(jobDescriptionId);

            if (company == null || resume == null || job == null)
            {
                return NotFound();
            }

            if (job.Resumes.FirstOrDefault(res => res.Id == resumeId) == null)
            {
                TempData["Error"] = "Resume has not apply for the job!";
                return RedirectToAction(nameof(Index));
            }

            var model = new CreateContractModel
            {
                CompanyId = company.Id,
                CompanyName = company.Name,
                ResumeId = resume.Id,
                ResumeName = resume.Name,
                JobDescriptionId = jobDescriptionId
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(CreateContractModel model)
        {
            if (_contractRepository.FindByCompanyIdAndResumeId(model.CompanyId, model.ResumeId) != null)
            {
                TempData["Error"] = "Resume has been had contract with this company!";
                return View(model);
            }

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
            var job = _jobDescriptionRepository.GetById(model.JobDescriptionId);

            if (company == null || resume == null || job == null)
            {
                return NotFound();
            }

            if (job.Resumes.FirstOrDefault(res => res.Id == resume.Id) == null)
            {
                TempData["Error"] = "Resume has not apply for the job!";
                return RedirectToAction(nameof(Index));
            }

            var contract = new Contract
            {
                Company = company,
                Resume = resume,
                InterviewTime = model.InterviewTime,
                Interviewer = model.Interviewer,
                OfferSalary = model.OfferSalary,
                RequestSalary = model.RequestSalary,
                Description = model.Description
            };

            _contractRepository.Insert(contract);
            TempData["Success"] = "Create contract successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Cancel(long contractId)
        {
            try
            {
                var contract = _contractRepository.GetById(contractId);
                if (contract == null)
                {
                    return NotFound();
                }

                _contractRepository.Delete(contractId);
                TempData["Success"] = "Cancel contract successfully!";

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occur. Try sgain";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
