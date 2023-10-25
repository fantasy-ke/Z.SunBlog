using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common;
using Z.Ddd.Common.Entities.Users;

namespace Z.SunBlog.EntityFrameworkCore.EntityFrameworkCore.Seed.SeedData
{
    public class DefaultUserBuilder
    {
        private readonly SunBlogDbContext _context;
        public DefaultUserBuilder(SunBlogDbContext dbContext)
        {
            _context = dbContext;
        }


        public void Create()
        {
            CreateDefaultUser();
        }

        private void CreateDefaultUser()
        {
            // 创建 Default 用户。

            var defaultUser = _context.ZUsers.IgnoreQueryFilters()
                .FirstOrDefault(t => t.UserName == ZConfigBase.DefaultUserName);
            if (defaultUser == null)
            {
                defaultUser = new ZUserInfo
                {
                    UserName = ZConfigBase.DefaultUserName,
                    PassWord = ZConfigBase.DefaultPassWord,
                };

                _context.ZUsers.Add(defaultUser);
                _context.SaveChanges();
            }
        }
    }
}
