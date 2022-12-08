using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace genshinwebsite.Services
{
    public class EmailVCodeDBService : IEmailVCodeDB
    {
        private readonly EmailVCodeDataContext _emailDataContext;
        private readonly IConfiguration _configuration;
        public EmailVCodeDBService(EmailVCodeDataContext emailVCodeDataContext, IConfiguration configuration)
        {
            _emailDataContext = emailVCodeDataContext;
            _configuration = configuration;
        }

        /// <summary>
        /// 生成新的邮箱验证码
        /// </summary>
        /// <returns></returns>
        private string make_new_VCode()
        {
            string vcode = "";
            var rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                vcode += rand.Next(0, 9).ToString();
            }
            return vcode;
        }
        public string get_VCode(string email)
        {
            string vcode = "";
            if(email == string.Empty || email == "" || email == null)
            {
                return string.Empty;
            }
            var record = _emailDataContext.EmailVcode.Find(email);
            if(record == null)
            {
                vcode = make_new_VCode();
                record = new Models.EmailVCodeModel
                {
                    Email = email,
                    RequestTime = 0,
                    VCode = vcode
                };
                _emailDataContext.Add(record);
                _emailDataContext.SaveChanges();
                
            }
            else
            {
                if((DateTime.Now - record.SendDateTime).TotalSeconds > _configuration.GetValue("VCodeAvailabeSpanSec", 120))
                {
                    vcode = make_new_VCode();
                    record.RequestTime = 0;
                    record.VCode = vcode;
                    record.SendDateTime = DateTime.Now;
                    _emailDataContext.Update(record);
                    _emailDataContext.SaveChanges();
                }
                else
                {
                    if(record.RequestTime > 5)
                    {
                        vcode = string.Empty;
                    }
                    else
                    {
                        vcode = record.VCode;
                        record.RequestTime += 1;
                        record.VCode = vcode;
                        _emailDataContext.Update(record);
                        _emailDataContext.SaveChanges();
                        
                    }
                    
                }
            }

            return vcode;
        }

        public bool is_code_verified(string email, string code)
        {
            var record = _emailDataContext.EmailVcode.Find(email);
            bool result = true;
            if (record == null)
            {
                result = false;
            }
            else if((DateTime.Now - record.SendDateTime).TotalSeconds > _configuration.GetValue("VCodeAvailabeSpanSec", 120))
            {
                var a = (DateTime.Now - record.SendDateTime).TotalSeconds;
                var b = _configuration.GetValue("VCodeAvailabeSpanSec", 120);
                result = false;
            }
            else if(code != record.VCode)
            {
                result = false;
            }
            
            return result;
        }

    }
}
