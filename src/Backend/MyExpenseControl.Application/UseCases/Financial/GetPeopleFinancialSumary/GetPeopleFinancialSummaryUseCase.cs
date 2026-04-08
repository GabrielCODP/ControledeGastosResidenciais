using AutoMapper;
using MyExpenseControl.Communication.Response.Financial.FinancialSummary;
using MyExpenseControl.Domain.Repositories.Financial;
using System;
using System.Net.Http.Headers;

namespace MyExpenseControl.Application.UseCases.Financial.GetPeopleFinancialSumary
{
    public class GetPeopleFinancialSummaryUseCase : IGetPeopleFinancialSummaryUseCase
    {

        private readonly IFinancialTransactionReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetPeopleFinancialSummaryUseCase(IFinancialTransactionReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponsePeopleFinancialSummaryJson> Execute()
        {
            var financialSumary = await _repository.GetUsersFinancialSummary();

            var people = _mapper.Map<IList<ResponsePersonFinancialSummaryJson>>(financialSumary);

            var totalRevenue = people.Sum(x => x.TotalRevenue);
            var totalExpense = people.Sum(x => x.TotalExpense);

            return new ResponsePeopleFinancialSummaryJson
            {
                People = people,
                Totals = new ResponseFinancialTotalsJson
                {
                    TotalRevenue = totalRevenue,
                    TotalExpense = totalExpense,
                    Balance = totalRevenue - totalExpense
                }
            };
        }
    }
}
