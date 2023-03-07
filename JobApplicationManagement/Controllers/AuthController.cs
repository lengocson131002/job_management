using JobApplicationManagement.Models.Auth;
using JobApplicationManagement.Utils;
using Microsoft.AspNetCore.Mvc;
using Repository.Enums;
using Repository.Models;
using Repository.Repositories.interfaces;
using System.Security.Cryptography;
using System.Text;

namespace JobApplicationManagement.Controllers
{
    public class AuthController : Controller
    {
        private IAccountRepository _accountRepository;

        public AuthController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), model);
            }

            Account? account = _accountRepository.GetByUsername(model.Username);
            if (account == null || !HashUtil.IsValid(model.Password, account.Password) || !Object.Equals(account.Status, AccountStatus.ACTIVE))
            {
                ViewBag.Error = "Incorrect Username Password";
                return View(nameof(Index));
            }
            HttpContext.Session.SetString("currentId", account.Id);
            HttpContext.Session.SetString("currentIsRootAdmin", account.IsRootAdmin + "");
            HttpContext.Session.SetString("currentName", account.FullName);
            HttpContext.Session.SetString("currentUsername", account.FullName);
            string? redirectUrl = HttpContext.Session.GetString("redirectUrl");
            if (redirectUrl != null)
            {
                HttpContext.Session.Remove("redirectUrl");
                return Redirect(redirectUrl);

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index));
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Auth");
        }

    }
}
