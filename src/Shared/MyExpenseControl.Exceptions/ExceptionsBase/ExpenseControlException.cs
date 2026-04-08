using System.Net;

namespace MyExpenseControl.Exceptions.ExceptionsBase
{
    public abstract class ExpenseControlException : SystemException
    {
        public ExpenseControlException(string message) : base(message)
        {
        }

        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
