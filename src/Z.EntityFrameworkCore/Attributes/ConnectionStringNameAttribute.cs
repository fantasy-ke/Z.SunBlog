using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.EntityFrameworkCore.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ConnectionStringNameAttribute : Attribute
{
    public readonly string ConnectionString;

    public ConnectionStringNameAttribute(string connectionString = "Default")
    {
        ConnectionString = connectionString;
    }
}
