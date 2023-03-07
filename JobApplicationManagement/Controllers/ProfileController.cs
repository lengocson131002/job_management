using JobApplicationManagement.Filters;
using JobApplicationManagement.Models.Auth;
using JobApplicationManagement.Utils;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.interfaces;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class ProfileController : Controller
    {
        private IAccountRepository _accountRepository;

        public ProfileController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult MyAccount()
        {
            string? id = HttpContext.Session.GetString("currentId");
            if (id == null)
            {
                TempData["Error"] = "Account not found";

            }
            Account account = _accountRepository.GetById(id);
            if (account == null)
            {
                return RedirectToAction(nameof(Index));
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
        [HttpPost]
        public IActionResult UpdateMyAccount(AccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(MyAccount), model);
            }
            Account? account = _accountRepository.GetByUsername(model.Username);
            if (account == null)
            {
                TempData["Error"] = "Account not found!";
                return RedirectToAction(nameof(MyAccount));
            }
            if (!String.IsNullOrEmpty(model.Email))
            {
                account.Email = model.Email;
            }
            if (!String.IsNullOrEmpty(model.FullName))
            {
                account.FullName = model.FullName;
            }
            if (!String.IsNullOrEmpty(model.Phone))
            {
                account.Phone = model.Phone;
            }
            if (!String.IsNullOrEmpty(model.Description))
            {
                account.Description = model.Description;
            }
            _accountRepository.Update(account);

            if (Object.Equals(account.Id, HttpContext.Session.GetString("currentId")))
            {
                HttpContext.Session.SetString("currentName", account.FullName);
            }

            TempData["Success"] = "Update successfully";
            return RedirectToAction(nameof(MyAccount));
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid || !Object.Equals(model.NewPassword, model.ConfirmPassword))
            {
                TempData["Error"] = "New Password and Confirm Password must be match";
                return View(nameof(ChangePassword));
            }
            string? id = HttpContext?.Session?.GetString("currentId");

            Account? account = _accountRepository.GetById(id);
            if (account == null)
            {
                return View(nameof(ChangePassword));
            }
            if (!HashUtil.IsValid(model.OldPassword, account?.Password))
            {

                TempData["Error"] = "Old Password incorrect";
                return View(nameof(ChangePassword));
            }
            account.Password = HashUtil.GetMD5(model.NewPassword);
            _accountRepository.Update(account);
            TempData["Success"] = "Update Successfully";
            return View(nameof(ChangePassword));
        }
    }
}
