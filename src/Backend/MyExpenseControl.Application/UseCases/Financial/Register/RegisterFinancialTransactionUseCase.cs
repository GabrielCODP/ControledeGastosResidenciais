using AutoMapper;
using MyExpenseControl.Communication.Requests.Financial;
using MyExpenseControl.Communication.Response.Financial;
using MyExpenseControl.Domain.Enum;
using MyExpenseControl.Domain.Repositories;
using MyExpenseControl.Domain.Repositories.Category;
using MyExpenseControl.Domain.Repositories.Financial;
using MyExpenseControl.Domain.Services.LoggedUser;
using MyExpenseControl.Exceptions;
using MyExpenseControl.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseControl.Application.UseCases.Financial.Register
{
    public class RegisterFinancialTransactionUseCase : IRegisterFinancialTransactionUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IFinancialTransactionWriteOnlyRepository _repository;
        private readonly ICategoryReadOnlyRepository _repositoryCategory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterFinancialTransactionUseCase(ILoggedUser loggedUser, IFinancialTransactionWriteOnlyRepository repository, ICategoryReadOnlyRepository repositoryCategory, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _repositoryCategory = repositoryCategory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisterFinancialTransactionJson> Execute(RequestRegisterFinancialTransactionJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();
            var category = await _repositoryCategory.GetById(loggedUser, request.CategoryId)
                                            ?? throw new NotFoundException(ResourceMessagesException.CATEGORY_NOT_FOUND);


            ValidateCategory(category, request);
            ValidateMinorCannotRegisterRevenue(loggedUser, request);

            var transaction = _mapper.Map<Domain.Entities.FinancialTransaction>(request);
            transaction.UserId = loggedUser.Id;

            await _repository.Add(transaction);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisterFinancialTransactionJson>(transaction);
        }

        private static void Validate(RequestRegisterFinancialTransactionJson request)
        {
            var result = new RegisterFinancialTransactionValidator().Validate(request);

            if (result.IsValid == false)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }

        private static void ValidateCategory(Domain.Entities.Category category, RequestRegisterFinancialTransactionJson financialTransaction)
        {

            if (category.Purpose != Purpose.Both && category.Purpose != (Purpose)financialTransaction.TransactionType)
            {
                throw new NotFoundException(ResourceMessagesException.CATEGORY_INCOMPATIBLE_WITH_TRANSACTION_TYPE);
            }
        }

        private static void ValidateMinorCannotRegisterRevenue(Domain.Entities.User user, RequestRegisterFinancialTransactionJson request)
        {
            if (user.Age < 18 && TransactionType.Revenue == (TransactionType)request.TransactionType)
            {
                throw new NotFoundException(ResourceMessagesException.MINOR_CANNOT_REGISTER_REVENUE);
            }
        }
    }
}
