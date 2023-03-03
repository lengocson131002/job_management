using JobApplicationManagement.Filters;
using JobApplicationManagement.Models.Resume;
using JobApplicationManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories;
using Repository.Repositories.interfaces;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class ResumeController : Controller
    {
        private ResumeRepository _resumeRepository;
        private IBaseRepository<Skill> _skillRepository;
        private IStorageService _storageSrvice;

        public ResumeController(
            ResumeRepository resumeRepository, 
            IBaseRepository<Skill> skillRepository,
            IStorageService storageService)
        {
            _resumeRepository = resumeRepository;
            _skillRepository = skillRepository;
            _storageSrvice = storageService;
        }


        public IActionResult Index()
        {
            List<Resume> resumes = _resumeRepository.GetAll().ToList();
            return View(resumes);
        }

        public IActionResult ViewResumes()
        {
            List<Resume> resumes = _resumeRepository.GetAll().ToList();
            return View(resumes);
        }

        [HttpGet]
        public IActionResult ViewDetailResume(long id)
        {
            Console.WriteLine(id);
            var resume = _resumeRepository.GetById(id);
            Console.WriteLine(resume.Description);
            ViewData["Skills"] = resume.Skills.ToList<Skill>();
            ViewData["FileUrl"] = _storageSrvice.getFullPathFile(resume.FileUrl);
            return View(resume);
        }

        [HttpGet]
        public IActionResult DeleteResume(long id)
        {
            Console.WriteLine(id);
            _resumeRepository.Delete(id);
            return RedirectToAction("ViewResumes", "Resume");
        }

        [HttpGet]
        public IActionResult CreateResume()
        {
            ViewData["Skills"] = _skillRepository.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateResume(CreateResumeModel model)
        {
            var keys = ModelState.Keys;
            if (!ModelState.IsValid)
            {
                ViewData["Skills"] = _skillRepository.GetAll().ToList();
                return View(nameof(CreateResume), model);
            }
            var resume = new Resume(
            
                );
            resume.Name = model.Name;
            resume.Phone = model.Phone;
            resume.Email = model.Email;
            resume.Birthday = model.Birthday;
            resume.Description = model.Description;

            var skills = new List<Skill>();
            foreach (var skillId in model.SkillIds)
            {
                Skill skill = _skillRepository.GetById(skillId);
                if (skill == null)
                {
                    ModelState.AddModelError("SkillIds", "Skill not found");
                    ViewData["Skills"] = _skillRepository.GetAll().ToList();
                    return View(nameof(CreateResume), model);
                }
                skills.Add(skill);
            }

            resume.Skills = skills;

            await using var memoryStream = new MemoryStream();
            await model.File.CopyToAsync(memoryStream);

            var result = await _storageSrvice.UploadFileAsync(model.File.FileName, memoryStream, "prn211");

            resume.FileUrl = "/" + model.File.FileName;

            _resumeRepository.Insert(resume);
            return RedirectToAction("ViewResumes", "Resume");
        }

        [HttpGet]
        public IActionResult UpdateResume(long id)
        {
            ViewData["Skills"] = _skillRepository.GetAll().ToList();
            var entity = _resumeRepository.GetById(id);

            UpdateResumeModel model = new UpdateResumeModel();

            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Phone = entity.Phone;
            model.Email = entity.Email;
            model.Birthday = entity.Birthday;
            model.Description = entity.Description;
            model.SkillIds = entity.Skills.Select(skill => skill.Id).ToList();
            model.FileName = entity.FileUrl.Substring(1);
            model.FileUrl = _storageSrvice.getFullPathFile(entity.FileUrl);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateResume(UpdateResumeModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Skills"] = _skillRepository.GetAll().ToList();
                return View(nameof(UpdateResume), model);
            }

            var resume = _resumeRepository.GetById(model.Id);
            resume.Name = model.Name;
            resume.Phone = model.Phone;
            resume.Email = model.Email;
            resume.Birthday = model.Birthday;
            resume.FileUrl = "abc.png";
            resume.Description = model.Description;

            var skills = new List<Skill>();
            foreach (var skillId in model.SkillIds)
            {
                Skill skill = _skillRepository.GetById(skillId);
                if (skill == null)
                {
                    ModelState.AddModelError("SkillIds", "Skill not found");
                    return View(model);
                }
                skills.Add(skill);
            }

            resume.Skills = skills;

            await using var memoryStream = new MemoryStream();
            if (model.UpdatedFile != null)
            {
                await model.UpdatedFile.CopyToAsync(memoryStream);

                var result = await _storageSrvice.UploadFileAsync(model.UpdatedFile.FileName, memoryStream, "prn211");

                resume.FileUrl = "/" + model.UpdatedFile.FileName;
            }

            _resumeRepository.Update(resume);
            return RedirectToAction("ViewResumes", "Resume");
        }
    }
}
