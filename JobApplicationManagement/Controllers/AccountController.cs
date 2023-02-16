using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories;

namespace JobApplicationManagement.Controllers
{
    public class AccountController : Controller
    {
        private IBaseRepository<Account> _accountRepository;

        public AccountController(IBaseRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            List<Account> accounts = _accountRepository.GetAll().ToList();
            return View(accounts);
        }
    }
}
