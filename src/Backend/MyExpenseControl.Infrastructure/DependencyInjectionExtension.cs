using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyExpenseControl.Domain.Repositories;
using MyExpenseControl.Domain.Repositories.Category;
using MyExpenseControl.Domain.Repositories.Financial;
using MyExpenseControl.Domain.Repositories.User;
using MyExpenseControl.Domain.Security.Cryptography;
using MyExpenseControl.Domain.Security.Tokens;
using MyExpenseControl.Domain.Services.LoggedUser;
using MyExpenseControl.Infrastructure.DataAccess;
using MyExpenseControl.Infrastructure.DataAccess.Database;
using MyExpenseControl.Infrastructure.DataAccess.Repositories;
using MyExpenseControl.Infrastructure.Extensions;
using MyExpenseControl.Infrastructure.Security.Cryptography;
using MyExpenseControl.Infrastructure.Security.Tokens.Access.Generator;
using MyExpenseControl.Infrastructure.Security.Tokens.Access.Validator;
using MyExpenseControl.Infrastructure.Services.LoggedUser;
using System.Reflection;

namespace MyExpenseControl.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext_MySql(services, configuration);
            AddFluentMigrator_MySql(services, configuration);
            AddRepositories(services);
            AddPassword(services, configuration);
            AddTokens(services, configuration);
            AddLoggedUser(services);
        }
        private static void AddDbContext_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddDbContext<MyExpenseControlDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }

        private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                var connectionString = configuration.ConnectionString();

                //Vai procurar em todas as classes que tem o [Migration(xxx)]
                options.AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("MyExpenseControl.Infrastructure")).For.All();
            });
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserDeleteOnlyRepository, UserRepository>();
            services.AddScoped<ICategoryWriteOnlyRepository, CategoryRepository>();
            services.AddScoped<ICategoryReadOnlyRepository, CategoryRepository>();
            services.AddScoped<IFinancialTransactionWriteOnlyRepository, FinancialTransactionRepository>();
            services.AddScoped<IFinancialTransactionReadOnlyRepository, FinancialTransactionRepository>();

        }

        private static void AddLoggedUser(IServiceCollection services)
        {
            services.AddScoped<ILoggedUser, LoggedUser>();
        }

        private static void AddPassword(this IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped<IPasswordEncripter>(options => new Sha512Encripter(additionalKey!));
        }

        private static void AddTokens(this IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(opt => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(opt => new JwtTokenValidator(signingKey!));
        }
    }
}
