using ExpenseControl.API.Converters;
using ExpenseControl.API.Filters;
using ExpenseControl.API.Token;
using Microsoft.OpenApi.Models;
using MyExpenseControl.Application;
using MyExpenseControl.Domain.Security.Tokens;
using MyExpenseControl.Infrastructure;
using MyExpenseControl.Infrastructure.Extensions;
using MyExpenseControl.Infrastructure.Migrations;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    const string bearer = "Bearer";
    options.AddSecurityDefinition(bearer, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 324245abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = bearer
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "ouath2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }

    });
});

//Filtro global de exeções
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

//Injeção de dependencia
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

//Deixar nome das rotas com letras minusculas.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Acessar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

await app.RunAsync();

//Conectar ou verificar BD
void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnviroment())
    {
        return;
    }

    var connectionString = builder.Configuration.ConnectionString();

    //vai criar um scoppe para utilizar o serviço de injeção de dependencia
    var servicesScoppe = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DatabaseMigration.Migrate(connectionString, servicesScoppe.ServiceProvider);
}
