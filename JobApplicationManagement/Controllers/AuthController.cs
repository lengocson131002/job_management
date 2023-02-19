using JobApplicationManagement.Models.Auth;
using Microsoft.AspNetCore.Mvc;
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
            if (account == null || !Object.Equals(model.Password, account.Password))
            {
                ViewBag.Error = "Incorrect Username Password";
                return View(nameof(Index));
            }
            HttpContext.Session.SetString("currentId", account.Id);
            HttpContext.Session.SetString("currentIsRootAdmin", account.IsRootAdmin + "");
            HttpContext.Session.SetString("currentName", account.FullName);
            HttpContext.Session.SetString("currentUsername", account.FullName);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index));
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Auth");
        }

        private static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}
