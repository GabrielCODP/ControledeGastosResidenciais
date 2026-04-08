
using MyExpenseControl.Domain.Repositories;
using MyExpenseControl.Domain.Repositories.Category;
using MyExpenseControl.Domain.Services.LoggedUser;
using MyExpenseControl.Exceptions;
using MyExpenseControl.Exceptions.ExceptionsBase;

namespace MyExpenseControl.Application.UseCases.Category.Delete
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly ICategoryReadOnlyRepository _repositoryRead;
        private readonly ICategoryWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryUseCase(ILoggedUser loggedUser, ICategoryReadOnlyRepository repositoryRead, ICategoryWriteOnlyRepository repositoryWrite, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long categoryId)
        {
            var loggedUser = await _loggedUser.User();
            var category = await _repositoryRead.GetById(loggedUser, categoryId);

            if (category is null)
            {
                throw new NotFoundException(ResourceMessagesException.CATEGORY_NOT_FOUND);
            }

            await _repositoryWrite.Delete(categoryId);
            await _unitOfWork.Commit();
        }
    }
}
