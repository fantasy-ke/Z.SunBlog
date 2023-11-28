﻿using Z.Ddd.Common.AutoMapper;

namespace Z.Ddd.Common.Authorization.Authorize
{
    public interface IAuthorizeRegister
    {
        List<IAuthorizePermissionProvider> AuthorizeProviders { get; }
    }
}