using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Extensions;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.SqlServer.Extensions;
using Z.SunBlog.Core.CustomConfigModule;
using Z.SunBlog.Core.MenuModule;
using Z.SunBlog.Core.MenuModule.Dtos;

namespace Z.SunBlog.EntityFrameworkCore.EntityFrameworkCore.Seed.SeedData
{
    public class DefaultCustomconfigitemBuilde
    {
        private readonly SunBlogDbContext _context;
        public DefaultCustomconfigitemBuilde(SunBlogDbContext dbContext)
        {
            _context = dbContext;
        }


        public void Create()
        {
            CreateDefaultCustomconfigitem();
        }

        private void CreateDefaultCustomconfigitem()
        {
            var defaultCustomconfigitem = _context.CustomConfigItem.IgnoreQueryFilters().ToList(); 
            if (defaultCustomconfigitem == null || defaultCustomconfigitem.Count == 0)
            {
                var jsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Configs{Path.DirectorySeparatorChar}InitData{Path.DirectorySeparatorChar}Customconfigitem.txt");
                var pageFilterJson = File.ReadAllText(jsonFileName);
                var customConfigitem = JsonConvert.DeserializeObject<List<CustomConfigItem>>(pageFilterJson);
                if (customConfigitem != null && customConfigitem.Count > 0)
                {
                    _context.CustomConfigItem.AddRange(customConfigitem);
                    _context.SaveChanges();
                }

            }
        }

    }
}
