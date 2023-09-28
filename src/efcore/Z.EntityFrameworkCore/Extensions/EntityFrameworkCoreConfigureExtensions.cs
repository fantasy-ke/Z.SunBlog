using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.Entities.Auditing;
using Z.Ddd.Common.Entities.Users;

namespace Z.EntityFrameworkCore.Extensions;

public static class EntityFrameworkCoreConfigureExtensions
{
    public static void AddZCoreEntityConfigure<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : FullAuditedEntity<Guid>
    {
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasKey(x => x.Id);


        //builder.Property(x => x.ExtraProperties)
        //        .HasConversion(x => JsonSerializer.Serialize(x, new JsonSerializerOptions()),
        //            x => JsonSerializer.Deserialize<Dictionary<string, object>>(x, new JsonSerializerOptions()) ?? new Dictionary<string, object>());
    }

    public static void AddZCoreConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ZUserInfo>(builder =>
        {
            builder.ToTable("ZUsers");
            builder.Property(p=>p.Name).HasMaxLength(16);
            builder.Property(p=>p.UserName).HasMaxLength(16);
            builder.Property(p=>p.PassWord).HasMaxLength(512);
        });

    }

}
