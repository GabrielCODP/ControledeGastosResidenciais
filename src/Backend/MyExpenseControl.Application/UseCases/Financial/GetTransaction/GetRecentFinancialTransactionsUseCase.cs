using AutoMapper;
using MyExpenseControl.Communication.Response.Financial;
using MyExpenseControl.Domain.Repositories.Financial;
using MyExpenseControl.Domain.Services.LoggedUser;


namespace MyExpenseControl.Application.UseCases.Financial.GetTransaction
{
    public class GetRecentFinancialTransactionsUseCase : IGetRecentFinancialTransactionsUseCase
    {

        private readonly IFinancialTransactionReadOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;

        public GetRecentFinancialTransactionsUseCase(IFinancialTransactionReadOnlyRepository repository, ILoggedUser loggedUser, IMapper mapper)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseFinancialTransactionsJson> Execute()
        {
            var loggedUser = await _loggedUser.User();

            var financialTransactions = await _repository.GetRecentFinancialTransactions(loggedUser);

            return new ResponseFinancialTransactionsJson
            {
                Transactions = _mapper.Map<IList<ResponseShortFinancialTransactionJson>>(financialTransactions)
            };
        }
    }
}
