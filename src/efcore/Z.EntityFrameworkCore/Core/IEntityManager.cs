using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.DependencyInjection;

namespace Z.EntityFrameworkCore.Core
{
    public interface IEntityManager<TDbContext> : ITransientDependency 
        where TDbContext : ZDbContext<TDbContext>
    {
        /// <summary>
        /// 添加种子数据
        /// </summary>
        void DbSeed(Action<TDbContext> action);
    }
}
