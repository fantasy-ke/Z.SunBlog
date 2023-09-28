using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.Entities.KeyGenerator
{
    public class ZStringKeyGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            return Guid.NewGuid().ToString("N"); // 使用 GUID 生成字符串主键
        }

        public override bool GeneratesStableValues => false;
    }
}
