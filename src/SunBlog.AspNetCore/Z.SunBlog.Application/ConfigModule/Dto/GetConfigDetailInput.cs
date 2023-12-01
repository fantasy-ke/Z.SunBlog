using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.SunBlog.Application.ConfigModule.Dto
{
    public class GetConfigDetailInput
    {
        public Guid Id { get; set; }

        public Guid? ItemId { get; set; }
    }
}
