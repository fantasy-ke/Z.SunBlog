using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common;
using Z.Ddd.Common.Entities.Users;
using Z.SunBlog.Core.MenuModule;
using Z.SunBlog.Core.MenuModule.Dtos;

namespace Z.SunBlog.EntityFrameworkCore.EntityFrameworkCore.Seed.SeedData
{
    public class DefaultMenuBuilder
    {
        private readonly SunBlogDbContext _context;
        public DefaultMenuBuilder(SunBlogDbContext dbContext)
        {
            _context = dbContext;
        }


        public void Create()
        {
            CreateDefaultMenu();
        }

        private void CreateDefaultMenu()
        {
            var jsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Configs{Path.DirectorySeparatorChar}MenuDynamic.json");
            var pageFilterJson = File.ReadAllText(jsonFileName);

            var dynamicMenuList = JsonConvert.DeserializeObject<List<MenuCreateDto>>(pageFilterJson);

            var menuList = SplicingMenu(dynamicMenuList, new List<Menu>(), null);

            _context.Menu.AddRange(menuList);
            _context.SaveChanges();
        }

        private List<Menu> SplicingMenu(List<MenuCreateDto> mentDtos, List<Menu> menus, Guid? panentId)
        {
            foreach (var menudto in mentDtos)
            {
                var menu = new Menu()
                {
                    Id = Guid.NewGuid(),
                    RouteName = menudto.Name,
                    Component = menudto.Component,
                    ParentId = panentId,
                    Path = menudto.Path,
                    Type = menudto.Meta.Type,
                    IsKeepAlive = menudto.Meta.IsKeepAlive,
                    Icon = menudto.Meta.Icon,
                    IsVisible = !menudto.Meta.IsHide,
                    Link = menudto.Meta.IsLink,
                    Name = menudto.Meta.Title,
                    IsDeleted = false
                };
                menus.Add(menu);
                if (menudto.Children != null && menudto.Children.Count > 0)
                {
                    SplicingMenu(menudto.Children, menus, menu.Id);
                }
            }
            return menus;
        }
    }
}
