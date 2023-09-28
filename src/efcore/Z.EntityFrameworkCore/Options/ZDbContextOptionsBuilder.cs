using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.EntityFrameworkCore.Options
{
    public class ZDbContextOptionsBuilder
    {
        internal IServiceProvider? ServiceProvider { get; }

        internal bool EnableSoftDelete { get; }

        public DbContextOptionsBuilder DbContextOptionsBuilder { get; }

        public Type DbContextType { get; }

        public ZDbContextOptionsBuilder(ZDbContextOptions masaDbContextOptions, Func<DbContextOptionsBuilder>? configure = null)
        {
            ServiceProvider = masaDbContextOptions.ServiceProvider;
            EnableSoftDelete = masaDbContextOptions.EnableSoftDelete;
            DbContextType = masaDbContextOptions.DbContextType;
            DbContextOptionsBuilder = configure == null ? new DbContextOptionsBuilder(masaDbContextOptions) : configure.Invoke();
        }

        public ZDbContextOptionsBuilder(DbContextOptionsBuilder dbContextOptionsBuilder, ZDbContextOptions masaDbContextOptions)
            : this(masaDbContextOptions, () => dbContextOptionsBuilder)
        {
        }
    }
}
