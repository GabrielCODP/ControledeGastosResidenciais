using AutoMapper;
using MyExpenseControl.Communication.Requests.Category;
using MyExpenseControl.Communication.Requests.Financial;
using MyExpenseControl.Communication.Requests.User;
using MyExpenseControl.Communication.Response.Category;
using MyExpenseControl.Communication.Response.Financial;
using MyExpenseControl.Communication.Response.Financial.FinancialSummary;
using MyExpenseControl.Communication.Response.Users;

namespace MyExpenseControl.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<RequestRegisterCategoryJson, Domain.Entities.Category>();
            CreateMap<RequestRegisterFinancialTransactionJson, Domain.Entities.FinancialTransaction>();

        }
        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
            CreateMap<Domain.Entities.Category, ResponseRegisterCategoryJson>();
            CreateMap<Domain.Entities.Category, ResponseShortCategoryJson>();
            CreateMap<Domain.Entities.FinancialTransaction, ResponseRegisterFinancialTransactionJson>();
            CreateMap<Domain.Entities.FinancialTransaction, ResponseShortFinancialTransactionJson>();

            CreateMap<Domain.Entities.UserFinancialSummary, ResponsePersonFinancialSummaryJson>();
        }
    }
}
