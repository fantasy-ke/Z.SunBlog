using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Common.DomainServiceRegister.Domain;

public interface IDomainService : ITransientDependency
{
}
