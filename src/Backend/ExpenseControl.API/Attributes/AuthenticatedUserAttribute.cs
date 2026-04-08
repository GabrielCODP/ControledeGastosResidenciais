using ExpenseControl.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}
