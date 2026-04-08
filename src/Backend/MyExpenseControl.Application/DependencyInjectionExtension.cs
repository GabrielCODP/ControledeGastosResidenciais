using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyExpenseControl.Application.Services.AutoMapper;
using MyExpenseControl.Application.UseCases.Category.Delete;
using MyExpenseControl.Application.UseCases.Category.GetById;
using MyExpenseControl.Application.UseCases.Category.Register;
using MyExpenseControl.Application.UseCases.Financial.GetPeopleFinancialSumary;
using MyExpenseControl.Application.UseCases.Financial.GetTransaction;
using MyExpenseControl.Application.UseCases.Financial.Register;
using MyExpenseControl.Application.UseCases.User.Delete;
using MyExpenseControl.Application.UseCases.User.DoLogin;
using MyExpenseControl.Application.UseCases.User.Register;
using MyExpenseControl.Application.UseCases.User.Response;

namespace MyExpenseControl.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection service)
        {
            service.AddScoped(option => new MapperConfiguration(autoMapperOptions =>
            {
                autoMapperOptions.AddProfile(new AutoMapping());
            }
            ).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection service)
        {
            service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            service.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            service.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            service.AddScoped<IRequestDeleteUserUseCase, RequestDeleteUserUseCase>();

            service.AddScoped<IRegisterCategoryUseCase, RegisterCategoryUseCase>();
            service.AddScoped<IListRecentCategoriesUseCase, GetRecentCategoriesUseCase>();
            service.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

            service.AddScoped<IRegisterFinancialTransactionUseCase, RegisterFinancialTransactionUseCase>();
            service.AddScoped<IGetRecentFinancialTransactionsUseCase, GetRecentFinancialTransactionsUseCase>();
            service.AddScoped<IGetPeopleFinancialSummaryUseCase, GetPeopleFinancialSummaryUseCase>();
        }
    }
}
