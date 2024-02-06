using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMirtel21.Utils.HttpConnector.Exceptions
{
    public class PassThroughTokenNotFoundException : Exception
    {
        public PassThroughTokenNotFoundException(string? message) : base(message)
        {
        }
    }
}
