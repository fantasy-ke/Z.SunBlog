﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;

namespace Z.NetWiki.Application.UserModule
{
    public interface IUserAppService : IApplicationService
    {
        Task Create();
    }
}