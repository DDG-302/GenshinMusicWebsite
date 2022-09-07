using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
using genshinwebsite.Controllers;
using genshinwebsite.Services;
using genshinwebsite.ViewModels;

namespace genshinwebsite.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRepository<UserModel> _repository;
        public RegisterController(IRepository<UserModel> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Register(UserLoginViewModel userModel)
        {
            
            //ViewData["testdata"] = "1145141919810";
            string account = userModel.Account;
            //string name = userModel.Name;
            //_repository.Add(userModel);
            return Content(account);
        }
    }
}
