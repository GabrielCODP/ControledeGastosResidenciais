using MyExpenseControl.Communication.Requests.User;
using MyExpenseControl.Communication.Response;
using MyExpenseControl.Communication.Response.Users;
using MyExpenseControl.Domain.Repositories.User;
using MyExpenseControl.Domain.Security.Cryptography;
using MyExpenseControl.Domain.Security.Tokens;
using MyExpenseControl.Exceptions.ExceptionsBase;

namespace MyExpenseControl.Application.UseCases.User.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Password);
            var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword);

            return user == null
                ? throw new InvalidLoginException()
                : new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokenJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }

    }
}
