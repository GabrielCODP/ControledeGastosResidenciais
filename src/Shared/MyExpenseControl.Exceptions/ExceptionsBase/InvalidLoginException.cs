using System.Net;

namespace MyExpenseControl.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : ExpenseControlException
    {
        
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
        {

        }

        public override IList<string> GetErrorMessages()
        {
            return [Message];
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.Unauthorized;
        }
    }
}
