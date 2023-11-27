﻿using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using Z.Ddd.Common.DomainServiceRegister;

namespace Z.Ddd.Common.DynamicWebAPI;

public class ApplicationServiceControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        if (typeof(IApplicationService).IsAssignableFrom(typeInfo))
        {
            if (!typeInfo.IsInterface &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeInfo.IsPublic)
            {
                return true;
            }
        }

        return false;
    }
}