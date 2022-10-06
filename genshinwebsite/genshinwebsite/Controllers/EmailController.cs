using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Services;
using NETCore.MailKit.Core;

namespace genshinwebsite.Controllers
{

    public class EmailController : Controller
    {
        private readonly IEmailService _EmailService;
        private readonly IEmailVCodeDB _emailVCodeDB;

        public EmailController(IEmailService emailService, IEmailVCodeDB emailVCodeDB)
        {
            _EmailService = emailService;
            this._emailVCodeDB = emailVCodeDB;
        }

        public IActionResult Email(string email)
        {
            //try
            //{
                if(email == string.Empty || email == ""||email==null)
                {
                    return BadRequest();
                }
                ViewData["Message"] = "ASP.NET Core mvc send email example";
                var VCode = _emailVCodeDB.get_VCode(email);
                if(VCode == string.Empty)
                {
                    return Forbid();
                }
                _EmailService.Send(email, "注册验证码", VCode);

                return Ok();
            //}
            //catch
            //{
                return StatusCode(500);
            //}
            
        }
    }

}
