using JobApplicationManagement.Filters;
using JobApplicationManagement.Models.JobDescription;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories;
using Repository.Repositories.interfaces;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class JobDescriptionController : Controller
    {
        private readonly ILogger<JobDescriptionController> _logger;
        private JobDescriptionRepository _jobDescriptionRepository;
        private ContractRepository _contractRepository;
        private IBaseRepository<Company> _companyResposiotory;
        private IBaseRepository<Skill> _skillResposiotory;
        private ResumeRepository _resumeRepository;

        public JobDescriptionController(JobDescriptionRepository jobDescriptionRepository,
            IBaseRepository<Company> companyResposiotory,
            IBaseRepository<Skill> skillResposiotory,
            ILogger<JobDescriptionController> logger,
            ContractRepository contractRepository,
            ResumeRepository resumeRepository)
        {
            _jobDescriptionRepository = jobDescriptionRepository;
            _companyResposiotory = companyResposiotory;
            _skillResposiotory = skillResposiotory;
            _logger = logger;
            _contractRepository = contractRepository;
            _resumeRepository = resumeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var skills = _skillResposiotory.GetAll();
            var companies = _companyResposiotory.GetAll();

            ViewData["Skills"] = skills;
            ViewData["Companies"] = companies;

            return View();
        }

        public PartialViewResult SearchJobDescriptions(GetAllJobDescriptionModel model)
        {
            var jobsPageResult = _jobDescriptionRepository
               .GetAll(model.Query, model.CompanyId, model.Level, model.Type, model.SkillId, model.PageNumber, model.PageSize, false);

            var items = jobsPageResult.Items;

            ViewData["PageNumber"] = model.PageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(jobsPageResult.TotalItems * 1.0 / model.PageSize);

            return PartialView("_JobListView", items);
        }

        [HttpGet]
        public PartialViewResult GetJobDescription(long id)
        {
            var jobDetail = _jobDescriptionRepository.GetById(id);
            return PartialView("_JobDetailView", jobDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Companies"] = _companyResposiotory.GetAll().ToList();
            ViewData["Skills"] = _skillResposiotory.GetAll().ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateJobDescriptionModel model)
        {

            if (model.MinSalary != null && model.MaxSalary != null && model.MaxSalary <= model.MinSalary)
            {
                ModelState.AddModelError("MaxSalary", "Max Salary must be greater than Min Salary");
            }

            if (model.SkillIds?.Count == 0)
            {
                ModelState.AddModelError("SkillIds", "Required at least one skill");
            }

            DateTime today = DateTime.Today;
            if (model.ClosedAt != null && DateTime.Compare((DateTime)model.ClosedAt, today) < 0)
            {
                ModelState.AddModelError("ClosedAt", "Closed Date must be greater than today");
            }

            if (!ModelState.IsValid)
            {
                ViewData["Companies"] = _companyResposiotory.GetAll().ToList();
                ViewData["Skills"] = _skillResposiotory.GetAll().ToList();
                return View(model);
            }

            Company company = _companyResposiotory.GetById(model.CompanyId);
            if (company == null)
            {
                ModelState.AddModelError("CompanyId", "Company Not found");
                return View(model);
            }

            var skills = new List<Skill>();
            foreach (var skillId in model.SkillIds)
            {
                Skill skill = _skillResposiotory.GetById(skillId);
                if (skill == null)
                {
                    ModelState.AddModelError("SkillIds", "Skill not found");
                    return View(model);
                }
                skills.Add(skill);
            }

            var jobDescription = new JobDescription
            {
                Title = model.Title.Trim(),
                Description = model.Description.Trim(),
                RequiredNumber = model.RequiredNumber,
                MinSalary = model.MinSalary,
                MaxSalary = model.MaxSalary,
                Type = model.Type,
                Level = model.Level,
                Skills = skills,
                ClosedAt = model.ClosedAt,
                Company = company
            };
            try
            {
                _jobDescriptionRepository.Insert(jobDescription);
                TempData["Success"] = "Create job description successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create job description");
                TempData["Error"] = "Create job description failed. Try again!";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Update(long id)
        {
            var jobDescription = _jobDescriptionRepository.GetById(id);
            if (jobDescription == null)
            {
                return NotFound();
            }
            ViewData["Companies"] = _companyResposiotory.GetAll().ToList();
            ViewData["Skills"] = _skillResposiotory.GetAll().ToList();

            UpdateJobDescriptionModel model = new UpdateJobDescriptionModel
            {
                Id = id,
                Title = jobDescription.Title,
                Description = jobDescription.Description,
                RequiredNumber = jobDescription.RequiredNumber,
                MinSalary = jobDescription.MinSalary,
                MaxSalary = jobDescription.MaxSalary,
                Type = jobDescription.Type,
                Level = jobDescription.Level,
                ClosedAt = jobDescription.ClosedAt,
                CompanyId = jobDescription.CompanyId,
                SkillIds = jobDescription.Skills.Select(skill => skill.Id).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UpdateJobDescriptionModel model)
        {
            if (model.MinSalary != null && model.MaxSalary != null && model.MaxSalary <= model.MinSalary)
            {
                ModelState.AddModelError("MaxSalary", "Max Salary must be greater than Min Salary");
            }

            if (model.SkillIds?.Count == 0)
            {
                ModelState.AddModelError("SkillIds", "Required at least one skill");
            }

            DateTime today = DateTime.Today;
            if (model.ClosedAt != null && DateTime.Compare((DateTime)model.ClosedAt, today) < 0)
            {
                ModelState.AddModelError("ClosedAt", "Closed Date must be greater than today");
            }

            if (!ModelState.IsValid)
            {
                ViewData["Companies"] = _companyResposiotory.GetAll().ToList();
                ViewData["Skills"] = _skillResposiotory.GetAll().ToList();
                return View(model);
            }
            // Update data
            var jobDescription = _jobDescriptionRepository.GetById(model.Id);
            if (jobDescription == null)
            {
                return NotFound();
            }

            Company company = _companyResposiotory.GetById(model.CompanyId);
            if (company == null)
            {
                ModelState.AddModelError("CompanyId", "Company Not found");
                return View(model);
            }

            var skills = new List<Skill>();
            foreach (var skillId in model.SkillIds)
            {
                Skill skill = _skillResposiotory.GetById(skillId);
                if (skill == null)
                {
                    ModelState.AddModelError("SkillIds", "Skill not found");
                    return View(model);
                }
                skills.Add(skill);
            }

            jobDescription.Title = model.Title.Trim();
            jobDescription.Description = model.Description.Trim();
            jobDescription.RequiredNumber = model.RequiredNumber;
            jobDescription.MinSalary = model.MinSalary;
            jobDescription.MaxSalary = model.MaxSalary;
            jobDescription.Type = model.Type;
            jobDescription.Level = model.Level;
            jobDescription.Skills = skills;
            jobDescription.ClosedAt = model.ClosedAt;
            jobDescription.Company = company;

            try
            {
                _jobDescriptionRepository.Update(jobDescription);
                TempData["Success"] = "Update job description successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Update job description failed!";
                _logger.LogError("Failed to create job description");
            }

            return View(model);

        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            try
            {
                _jobDescriptionRepository.Delete(id);
                TempData["Success"] = "Delete job description successfully!";
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create job description: ");
                TempData["Error"] = "Delete job description failed!. Try again";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ViewDetail(long id)
        {
            var job = _jobDescriptionRepository.GetById(id);
            if (job == null)
            {
                return NotFound();
            }

            ViewData["Resumes"] = _resumeRepository
                .GetBySkills(job.Skills.Select(skill => skill.Id).ToList())
                .Where(res => job.Resumes.FirstOrDefault(r => r.Id == res.Id) == null)
                .ToList();

            return View(job);
        }

        [HttpGet]
        public IActionResult ChooseResume(long id, long resumeId)
        {
            var job = _jobDescriptionRepository.GetById(id);
            if (job == null)
            {
                return NotFound();
            }
            var resume = _resumeRepository.GetById(resumeId);
            if (resume == null)
            {
                return NotFound();
            }

            if (job.Resumes.FirstOrDefault(res => res.Id == resumeId) != null) {
                TempData["Error"] = "Existed resume.";
            } else
            {
                job.Resumes.Add(resume);
                _jobDescriptionRepository.Update(job);
                TempData["Success"] = "Choose resume successfully";
            }

            return RedirectToAction(nameof(ViewDetail), new { id });
        }

        [HttpGet]
        public IActionResult RemoveResume(long id, long resumeId)
        {
            var job = _jobDescriptionRepository.GetById(id);
            if (job == null)
            {
                return NotFound();
            }
            var resume = _resumeRepository.GetById(resumeId);
            if (resume == null)
            {
                return NotFound();
            }

            if (job.Resumes.FirstOrDefault(res => res.Id == resumeId) == null)
            {
                TempData["Error"] = "UnExisted resume.";
            }
            else
            {
                job.Resumes = job.Resumes.Where(res => res.Id != resumeId).ToList();
                _jobDescriptionRepository.Update(job);
                TempData["Success"] = "Remove resume from job description successfully";
            }

            return RedirectToAction(nameof(ViewDetail), new { id });
        }
    }


}
