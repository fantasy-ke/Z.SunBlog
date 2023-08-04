using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Domain.DependencyInjection;
using Z.Ddd.Domain.Entities;
using Z.Ddd.Domain.Entities.Auditing;
using Z.Ddd.Domain.Entities.IAuditing;
using Z.Ddd.Domain.Extensions;
using Z.Ddd.Domain.Helper;

namespace Z.EntityFrameworkCore
{
    public abstract class ZDbContext<TDbContext> : DbContext 
        where TDbContext : DbContext
    {
        private IServiceScope? _serviceScope;
        public IZLazyServiceProvider _lazyServiceProvider;
        public IAuditPropertySetter AuditPropertySetter => _lazyServiceProvider.LazyGetRequiredService<IAuditPropertySetter>();

        protected ZDbContext(DbContextOptions<TDbContext> options)
        : base(options)
        {
            _serviceScope = ServiceProviderCache.Instance.GetOrAdd(options, providerRequired: true)
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope();

            _lazyServiceProvider = _serviceScope.ServiceProvider.GetRequiredService<IZLazyServiceProvider>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await  base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        //public virtual void Initialize(AbpEfCoreDbContextInitializationContext initializationContext)
        //{
        //    if (initializationContext.UnitOfWork.Options.Timeout.HasValue &&
        //        Database.IsRelational() &&
        //        !Database.GetCommandTimeout().HasValue)
        //    {
        //        Database.SetCommandTimeout(TimeSpan.FromMilliseconds(initializationContext.UnitOfWork.Options.Timeout.Value));
        //    }

        //    ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;

        //    ChangeTracker.Tracked += ChangeTracker_Tracked;
        //    ChangeTracker.StateChanged += ChangeTracker_StateChanged;

        //}


        protected virtual void ChangeTracker_Tracked(object sender, EntityTrackedEventArgs e)
        {
            PublishEventsForTrackedEntity(e.Entry);
        }

        protected virtual void ChangeTracker_StateChanged(object sender, EntityStateChangedEventArgs e)
        {
            PublishEventsForTrackedEntity(e.Entry);
        }

        private void PublishEventsForTrackedEntity(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyAbpConceptsForAddedEntity(entry);
                    break;
                case EntityState.Modified:

                    break;
                case EntityState.Deleted:
                    ApplyAbpConceptsForDeletedEntity(entry);
                    break;
            }
        }


        protected virtual void ApplyAbpConceptsForAddedEntity(EntityEntry entry)
        {
            CheckAndSetId(entry);
            SetCreationAuditProperties(entry);
        }

        protected virtual void ApplyAbpConceptsForDeletedEntity(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }


            entry.Reload();
            ObjectPropertyHelper.TrySetProperty(entry.Entity.As<ISoftDelete>(), x => x.IsDeleted, () => true);
            SetDeletionAuditProperties(entry);
        }


        protected virtual void SetCreationAuditProperties(EntityEntry entry)
        {
           AuditPropertySetter?.SetCreationProperties(entry.Entity);
        }

        protected virtual void SetDeletionAuditProperties(EntityEntry entry)
        {
           AuditPropertySetter?.SetDeletionProperties(entry.Entity);
        }

        protected virtual void CheckAndSetId(EntityEntry entry)
        {
            if (entry.Entity is IEntity<Guid> entityWithGuidId)
            {
                TrySetGuidId(entry, entityWithGuidId);
            }
        }


        protected virtual void TrySetGuidId(EntityEntry entry, IEntity<Guid> entity)
        {
            if (entity.Id != default)
            {
                return;
            }

            var idProperty = entry.Property("Id").Metadata.PropertyInfo;


            EntityHelper.TrySetId(
                entity,
                () => Guid.NewGuid(),
                true
            );
        }

    }
}
