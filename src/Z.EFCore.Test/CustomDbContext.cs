using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.Attributes;
using Z.Module.DependencyInjection;

namespace Z.EFCore.Test;

[ConnectionStringName("App:ConnectionString:Default")]
public class CustomDbContext : ZDbContext
{
    public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
    {
    }
}
