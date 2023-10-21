using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.SunBlog.Application.UserModule.Dto
{
    public class ZUserInfoDto
    {
        public string? Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? PassWord { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string? Code { get; set; }
    }
}
