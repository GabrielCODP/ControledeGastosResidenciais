using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseControl.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : ExpenseControlException
    {
        private readonly IList<string> _errorMessages;

        public ErrorOnValidationException(IList<string> errosMessages) : base(string.Empty)
        {
            _errorMessages = errosMessages;
        }

        public override IList<string> GetErrorMessages()
        {
            return _errorMessages;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}
