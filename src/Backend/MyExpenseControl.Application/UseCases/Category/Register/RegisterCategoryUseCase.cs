using AutoMapper;
using MyExpenseControl.Communication.Requests.Category;
using MyExpenseControl.Communication.Response.Category;
using MyExpenseControl.Domain.Repositories;
using MyExpenseControl.Domain.Repositories.Category;
using MyExpenseControl.Domain.Services.LoggedUser;
using MyExpenseControl.Exceptions.ExceptionsBase;



namespace MyExpenseControl.Application.UseCases.Category.Register
{
    public class RegisterCategoryUseCase : IRegisterCategoryUseCase
    {
        private readonly ICategoryWriteOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCategoryUseCase(ICategoryWriteOnlyRepository repository, ILoggedUser loggedUser, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisterCategoryJson> Execute(RequestRegisterCategoryJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var category = _mapper.Map<Domain.Entities.Category>(request);

            category.UserId = loggedUser.Id;

            await _repository.Add(category);
            await _unitOfWork.Commit();

           return _mapper.Map<ResponseRegisterCategoryJson>(category);
        }

        private static void Validate(RequestRegisterCategoryJson request)
        {
            var result = new RegisterCategoryValidator().Validate(request);

            if (result.IsValid == false)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }
    }
}
