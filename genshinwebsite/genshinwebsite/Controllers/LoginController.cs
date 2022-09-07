using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;

namespace genshinwebsite.Controllers
{

    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("login2");
        }

        public IActionResult a()
        {
            return Content("test a");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // 禁止跨域post
        public IActionResult user_login(string account, string password)
        {
            //return View("home/index");
            //var user = new UserModel
            //{
            //    Account = account,
            //    Password = password
            //};
            return Content("account: " + account + "\npassword: " +password);
        }

        [Route("register")]
        public IActionResult register()
        {
            //return View("home/index");

            return View("../register/register");
        }
    }
}
