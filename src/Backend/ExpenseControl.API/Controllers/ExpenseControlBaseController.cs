using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpenseControlBaseController : ControllerBase
    {
    }
}
