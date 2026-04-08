using MyExpenseControl.Communication.Requests.User;
using MyExpenseControl.Communication.Response.Users;

namespace MyExpenseControl.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
