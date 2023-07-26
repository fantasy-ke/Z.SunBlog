using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Domain.Entities.Auditing;

namespace Z.EntityFrameworkCore
{
    public abstract class ZDbContext<TDbContext> : DbContext 
        where TDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        protected virtual void SetCreationAuditProperties(EntityEntry entry)
        {
           // AuditPropertySetter?.SetCreationProperties(entry.Entity);
        }

        protected virtual void SetDeletionAuditProperties(EntityEntry entry)
        {
          //  AuditPropertySetter?.SetDeletionProperties(entry.Entity);
        }

    }
}
