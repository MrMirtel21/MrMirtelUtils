using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMirtel21.Utils.HttpConnector.Exceptions
{
    public class AuthTokenNotDeserializedException : Exception
    {
        public AuthTokenNotDeserializedException()
        {
            
        }

        public AuthTokenNotDeserializedException(string? message) : base(message)
        {
        }
    }
}
