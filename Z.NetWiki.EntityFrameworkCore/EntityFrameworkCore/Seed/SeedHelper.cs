using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Z.Ddd.Common.UnitOfWork;

namespace Z.NetWiki.EntityFrameworkCore.EntityFrameworkCore.Seed;

public static class SeedHelper
{
    public static void SeedHostDb(IServiceProvider serviceProvider)
    {
        WithDbContext<NetWikiDbContext>(serviceProvider, SeedHostDb);
    }

    public static void SeedHostDb(NetWikiDbContext context)
    {
    }

    private static async void WithDbContext<TDbContext>(IServiceProvider serviceProvider, Action<TDbContext> contextAction)
            where TDbContext : DbContext
    {
        using (var uowManager = serviceProvider.GetRequiredService<IUnitOfWork>())
        {
            using (var uow = await uowManager.BeginTransactionAsync())
            {
                //var context = await uowManager.GetDbContext();

                //contextAction(context);

                await uow.CommitAsync();
            }
        }
        
        
    }
}
