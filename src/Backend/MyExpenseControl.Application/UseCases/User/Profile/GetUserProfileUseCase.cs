using AutoMapper;
using MyExpenseControl.Communication.Response.Users;
using MyExpenseControl.Domain.Repositories.User;
using MyExpenseControl.Domain.Security.Tokens;
using MyExpenseControl.Domain.Services.LoggedUser;

namespace MyExpenseControl.Application.UseCases.User.Response
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;

        public GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _loggedUser.User();
            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
