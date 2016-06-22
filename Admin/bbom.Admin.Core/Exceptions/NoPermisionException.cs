using System;

namespace bbom.Admin.Core.Exceptions
{
    public class NoPermisionException : Exception
    {
        public override string Message
        {
            get { return "Недостаточно прав доступа"; }
        }
    }
}