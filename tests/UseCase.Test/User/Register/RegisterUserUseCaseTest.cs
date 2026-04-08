using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repository;
using CommonTestUtilities.Requests.User;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using MyExpenseControl.Application.UseCases.User.Register;
using MyExpenseControl.Exceptions;
using MyExpenseControl.Exceptions.ExceptionsBase;

namespace UseCase.Test.User.Register
{
    //Teste de unidade para UseCase
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var userCase = CreateUseCase();
            var result = await userCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var userCase = CreateUseCase(request.Email);

            //Guardando uma função dentro de uma variável
            Func<Task> act = async () => await userCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.GetErrorMessages().Count == 1 &&
                   e.GetErrorMessages().Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        }


        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var userCase = CreateUseCase();

            Func<Task> act = async () => await userCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public async Task Error_Age_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Age = 0;
            var userCase = CreateUseCase();

            Func<Task> act = async () => await userCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                 .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessagesException.AGE_INVALID));
        }

        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var passwordEncripiter = PasswordEncripterBuilder.Build();
            var mapper = MapperBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var accesTokenGenerator = JwtTokenGeneratorBuilder.Build();

            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();


            if (string.IsNullOrEmpty(email) == false)
            {
                readRepositoryBuilder.ExistActiveUserWithEmail(email);
            }

            return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), passwordEncripiter, mapper, unitOfWork, accesTokenGenerator);
        }
    }
}
