using MyExpenseControl.Communication.Response.Users;

namespace MyExpenseControl.Application.UseCases.User.Response
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
