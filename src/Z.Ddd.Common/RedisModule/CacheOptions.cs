using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.RedisModule.CacheHelper;

namespace Z.Ddd.Common.RedisModule
{
    public class CacheOptions
    {
        public CacheType CacheType { get; set; }
        public string RedisEndpoint { get; set; }
    }
}
