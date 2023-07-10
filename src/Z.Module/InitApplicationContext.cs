using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Module
{
    public class InitApplicationContext
    {
        public IServiceProvider ServiceProvider { get; set; }

        public InitApplicationContext(IServiceProvider serviceProvider)
        {

            ServiceProvider = serviceProvider;
        }
    }
}
