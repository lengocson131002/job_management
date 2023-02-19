using JobApplicationManagement.Filters;
using JobApplicationManagement.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Repository.Enums;
using Repository.Models;
using Repository.Repositories.interfaces;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            List<Account> accounts = _accountRepository.GetAll().ToList();
            return View(accounts);
        }

        [HttpGet]
        [Route("AccountDetail/{id}")]
        public IActionResult AccountDetail(string id)
        {
            Account account = _accountRepository.GetById(id);
            if (account == null)
            {
                return RedirectToAction("Index");
            }
            AccountModel accountModel = new AccountModel()
            {
                Id = account.Id,
                Description = account.Description,
                FullName = account.FullName,
                Username = account.Username,
                Email = account.Email,
                Phone = account.Phone,
                Status = account.Status,
                IsRootAdmin = account.IsRootAdmin
            };


            return View(accountModel);
        }

        public IActionResult Inactivate(DeleteAccountModel model)
        {
            string? id = HttpContext.Session.GetString("currentId");
            if (id == null || Object.Equals(model.Id, id))
            {
                TempData["Error"] = "Cannot inactivate your self";
                return RedirectToAction(nameof(Index));
            }
            Account account = _accountRepository.GetById(model.Id);
            if (account != null)
            {
                if (account.IsRootAdmin != null && (bool)account.IsRootAdmin)
                {
                    TempData["Error"] = "Cannot inactivate root admin";
                    return RedirectToAction(nameof(Index));
                }
                account.Status = AccountStatus.INACTIVE;
                _accountRepository.Update(account);
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Activate(UpdateAccountModel model)
        {
            Account account = _accountRepository.GetById(model.Id);
            if (account != null)
            {
                account.Status = AccountStatus.ACTIVE;
                _accountRepository.Update(account);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(AddAccount), model);
            }

            Account? account = _accountRepository.GetByUsername(model.Username);
            if (account != null)
            {
                TempData["Error"] = "Existed Username";
                return RedirectToAction(nameof(AddAccount));
            }
            Guid myuuid = Guid.NewGuid();
            account = new Account()
            {
                Id = myuuid.ToString(),
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                FullName = model.FullName,
                Phone = model.Phone,
                IsRootAdmin = false,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                Status = AccountStatus.ACTIVE,
            };
            _accountRepository.Insert(account);
            TempData["Success"] = "Add successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
