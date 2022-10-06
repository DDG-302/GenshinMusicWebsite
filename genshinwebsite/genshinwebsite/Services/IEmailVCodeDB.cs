using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genshinwebsite.Services
{
    public interface IEmailVCodeDB
    {
        /// <summary>
        /// 判断用户输入的code是否正确
        /// </summary>
        /// <param name="email">用户的邮箱</param>
        /// <param name="code">用户输入验证码</param>
        /// <returns></returns>
        public bool is_code_verified(string email, string code);

        /// <summary>
        /// 获取VCode
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string get_VCode(string email);

    }
}
