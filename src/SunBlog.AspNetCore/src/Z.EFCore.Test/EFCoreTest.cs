using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Z.Ddd.Common.Entities.IAuditing;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.UnitOfWork;
using Z.EntityFrameworkCore.Attributes;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.Core;
using Z.EntityFrameworkCore.SqlServer.Extensions;
using Z.Module;
using System.Reflection;
using Z.Ddd.Common.Entities.Auditing;
using Microsoft.AspNetCore.Builder;
using Z.Module.Extensions;

namespace Z.EFCore.Test
{
    [TestClass]
    public class EFCoreTest
    {
        private UnitOfWork<CustomDbContext> _unitOfWork;
        private CustomDbContext _dbContext;

        [TestInitialize]
        public  void Initialize()
        {
            var builder = WebApplication.CreateBuilder();


            builder.Services.AddApplication<ZTestModule>();
            var services = new ServiceCollection();

            var connectString = typeof(CustomDbContext).GetCustomAttribute<ConnectionStringNameAttribute>();
            var confContext = new ServiceConfigerContext(services);
            //services.AddSingleton(typeof(IConfiguration),typeof(ConfigurationBuilder));
            confContext.AddSqlServerEfCoreEntityFrameworkCore<CustomDbContext>();
            services.AddSingleton(typeof(IAuditPropertySetter), typeof(AuditPropertySetter));
            var serviceProvider = services.BuildServiceProvider();
            _dbContext = serviceProvider.GetRequiredService<CustomDbContext>();
            
            var auditPropertySetter = serviceProvider.GetRequiredService<IAuditPropertySetter>();
            //_dbContext.Database.EnsureCreated();
            _unitOfWork = new UnitOfWork<CustomDbContext>(_dbContext, auditPropertySetter);
            
        }

        [TestMethod]
        public async Task TestUnitOfWorkAccessorAsync()
        {
            await _unitOfWork.BeginTransactionAsync();

            var user = new ZUserInfo()
            {
                Name = Guid.NewGuid().ToString()
            };
            _dbContext.Add(user);
            await _unitOfWork.CommitTransactionAsync();
        }
    }

}
