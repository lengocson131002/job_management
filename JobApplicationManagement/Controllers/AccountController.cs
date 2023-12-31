﻿using JobApplicationManagement.Filters;
using JobApplicationManagement.Models.Auth;
using JobApplicationManagement.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Enums;
using Repository.Models;
using Repository.Repositories;
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
        [Route("Account/AccountDetail/{id}")]
        public IActionResult AccountDetail(string id)
        {
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

        public IActionResult Inactivate(InactivateAccountModel model)
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


        public IActionResult Activate(ActivateAccountModel model)
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
            if (!Object.Equals(model.Password, model.ConfirmPassword))
            {
                ModelState.TryAddModelError("ConfirmPassword", "Password and Confirm Password must be match");
            }
            if (!ModelState.IsValid)
            {
                return View(nameof(AddAccount), model);
            }
            Account? account = _accountRepository.GetByUsername(model.Username);
            if (account != null)
            {
                TempData["Error"] = "Existed Username";
                return View(nameof(AddAccount), model);
            }
            Guid myuuid = Guid.NewGuid();
            account = new Account()
            {
                Id = myuuid.ToString(),
                Username = model.Username,
                Password = HashUtil.GetMD5(model.Password),
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

        [HttpPost]
        public IActionResult Update(AccountModel model)
        {
            Account? account = _accountRepository.GetByUsername(model.Username);
            if (account == null)
            {
                TempData["Error"] = "Account not found!";
                return RedirectToAction(nameof(AddAccount));
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
                HttpContext.Session.SetString("currentName", model.FullName);
            }

            TempData["Success"] = "Update successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public PartialViewResult GetAccounts(GetAccountsModel model)
        {
            var jobsPageResult = _accountRepository
               .GetAll(model.Query, model.Status, model.PageNumber, model.PageSize);

            ViewData["PageNumber"] = model.PageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(jobsPageResult.TotalItems * 1.0 / model.PageSize);

            List<Account> accounts = jobsPageResult.Items;
            return PartialView("_AccountListView", accounts);
        }
    }
}
