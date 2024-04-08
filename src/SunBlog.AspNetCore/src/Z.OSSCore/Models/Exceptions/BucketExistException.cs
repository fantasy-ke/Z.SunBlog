using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.OSSCore.Models.Exceptions
{
    public class BucketExistException : Exception
    {
        public BucketExistException()
        {

        }

        public BucketExistException(string message) : base(message)
        {

        }

        public BucketExistException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
