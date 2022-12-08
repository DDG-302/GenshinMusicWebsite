using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
using genshinwebsite.Services;
using genshinwebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using genshinwebsite.Controllers.music;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;

namespace genshinwebsite.Controllers
{
    public class ForgetPasswordController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _EmailService;
        private readonly IEmailVCodeDB _emailVCodeDB;

        public ForgetPasswordController(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager, IConfiguration configuration, IEmailService emailService, IEmailVCodeDB emailVCodeDB)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _EmailService = emailService;
            _emailVCodeDB = emailVCodeDB;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsEmailValid(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                return Ok(false);
            }
            else
            {
                
                return Ok(true);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UpdatePasswordModel updatePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(403, ModelState.Values.First().ToString());
            }
            var user = await _userManager.FindByEmailAsync(updatePasswordModel.Account);

            if (user is null)
            {
                return StatusCode(403, "用户不存在");
            }
            else
            {
                if (! _emailVCodeDB.is_code_verified(updatePasswordModel.Account, updatePasswordModel.VCode))
                {
                    return StatusCode(403, "邮箱验证码错误");
                }
                else
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, updatePasswordModel.Password);
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        return StatusCode(500, "更新错误");
                    }
                }
                
            }
            return Ok("修改成功");
        }
    }
}
