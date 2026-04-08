using AutoMapper;
using MyExpenseControl.Communication.Response.Category;
using MyExpenseControl.Domain.Repositories.Category;
using MyExpenseControl.Domain.Services.LoggedUser;

namespace MyExpenseControl.Application.UseCases.Category.GetById
{
    public class GetRecentCategoriesUseCase : IListRecentCategoriesUseCase
    {
        private readonly ICategoryReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public GetRecentCategoriesUseCase(ICategoryReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
        {
            _repository = repository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseCategoriesJson> Execute()
        {
            var loggedUser = await _loggedUser.User();

            var categories = await _repository.GetRecentlyCreatedCategories(loggedUser);

            return new ResponseCategoriesJson
            {
                Categories = _mapper.Map<IList<ResponseShortCategoryJson>>(categories)
            };
        }

       
    }
}
