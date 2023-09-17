using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Common.Authorization.Authorize
{

    public class AuthorizeManager : IAuthorizeManager
    {
        public async Task AddAuthorizeRegiester()
        {
           await Task.FromResult(0);
        }
    }

    public interface IAuthorizeManager:ITransientDependency
    {
        Task AddAuthorizeRegiester();
    }
}
