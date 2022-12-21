using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApplication.Models;
using Helper;

namespace ShoppingApplication.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IValidationHelper _validationHelper;
        public RegisterController(IValidationHelper validationHelper)
        {
            _validationHelper = validationHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserDetails userDetails)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string validationMessage = string.Empty;
            if (!_validationHelper.IsAgeOrDobSupplied(userDetails.Age, userDetails.DateOfBirth, ref validationMessage))
            {
                ModelState.AddModelError("Age", validationMessage);
                ModelState.AddModelError("DateOfBirth", validationMessage);
                return View();
            }
            if (userDetails.Age.HasValue && userDetails.DateOfBirth.HasValue)
            {
                validationMessage = "Enter either age or date of birth"; //not both
                ModelState.AddModelError("Age", validationMessage);
                ModelState.AddModelError("DateOfBirth", validationMessage);
                return View();
            }
            if (userDetails.Age.HasValue
                && !_validationHelper.IsValidAge(userDetails.Age.Value, ref validationMessage))
            {
                ModelState.AddModelError("Age", validationMessage);
                return View();
            }
            if (userDetails.DateOfBirth.HasValue
                && !_validationHelper.IsValidDateOfBirth(userDetails.DateOfBirth.Value, ref validationMessage))
            {
                ModelState.AddModelError("DateOfBirth", validationMessage);
                return View();
            }

            // In live system, data needs to be Saved to DB

            return View("Thanks", userDetails);
        }
    }
}
