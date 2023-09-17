using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.EntityFrameworkCore.Options
{
    internal class ZDbContextOptions
    {
        public static string Name = "ConnectionStrings";

        /// <summary>
        /// 连接字符串
        /// </summary>
        [Required(ErrorMessage = "连接字符串是必须的")]
        public string Default { get; set; } = null!;

        /// <summary>
        /// 是否启用软删过滤器
        /// </summary>
        public bool SoftDelete { get; set; } = true;
    }
}
