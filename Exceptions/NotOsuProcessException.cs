using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuToolsV2.Exceptions
{
    public class NotOsuProcessException : osuToolsException
    {
        protected NotOsuProcessException()
        {
        }

        public NotOsuProcessException(string msg) : base(msg)
        {
        }

        public NotOsuProcessException(string msg, Exception? innerException) : base(msg, innerException)
        {
        }
    }
}
