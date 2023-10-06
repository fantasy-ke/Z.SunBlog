using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.AutoMapper;

namespace Z.Ddd.Common.DomainServiceRegister;

public abstract class ZServiceBase
{
    /// <summary>
    /// Reference to the object to object mapper.
    /// </summary>
    public IMapper ObjectMapper { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ZServiceBase(IServiceProvider serviceProvider)
    {
        ObjectMapper = serviceProvider.GetRequiredService<IMapper>();
    }
}
