
using MyExpenseControl.Communication.Requests.User;
using MyExpenseControl.Communication.Response.Users;

namespace MyExpenseControl.Application.UseCases.User.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
