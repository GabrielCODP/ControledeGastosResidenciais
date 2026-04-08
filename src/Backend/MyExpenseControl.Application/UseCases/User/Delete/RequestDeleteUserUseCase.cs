
using MyExpenseControl.Domain.Repositories;
using MyExpenseControl.Domain.Repositories.User;
using MyExpenseControl.Domain.Services.LoggedUser;

namespace MyExpenseControl.Application.UseCases.User.Delete
{
    public class RequestDeleteUserUseCase : IRequestDeleteUserUseCase
    {
        private readonly IUserDeleteOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public RequestDeleteUserUseCase(IUserDeleteOnlyRepository repository, ILoggedUser loggedUser, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute()
        {
            var loggedUser = await _loggedUser.User();
            _repository.DeleteAccount(loggedUser);

            await _unitOfWork.Commit();
        }
    }
}
