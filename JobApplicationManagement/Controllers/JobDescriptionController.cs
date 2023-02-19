<<<<<<< HEAD
﻿using JobApplicationManagement.Filters;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> origin/develop
using Repository.Repositories;

namespace JobApplicationManagement.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class JobDescriptionController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
