using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.DependencyInjection;

namespace Z.EntityFrameworkCore.Core
{
    public class EntityManager<TDbContext> : IEntityManager<TDbContext>, ITransientDependency
        where TDbContext : ZDbContext<TDbContext>
    {
        private readonly IServiceProvider _serviceProvider;
        public EntityManager(TDbContext dbContext, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void DbSeed(Action<TDbContext> action)
        {
            using var scope = _serviceProvider.CreateScope();
            var _dbcontext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            action.Invoke(_dbcontext);
        }
    }
}
