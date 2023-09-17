
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Z.NetWiki.EntityFrameworkCore.ConfigureExtensions;

public static class SimpleEntityFrameworkCoreExtensions
{
    public static ModelBuilder ConfigureModel(this ModelBuilder builder)
    {
        //builder.Entity<UserInfo>(x =>
        //{
        //    x.ToTable("UserInfos");

        //    x.AddSimpleConfigure();

        //});

        return builder;
    }

}