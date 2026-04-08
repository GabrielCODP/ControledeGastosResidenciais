using Microsoft.EntityFrameworkCore;
using MyExpenseControl.Domain.Entities;
using MyExpenseControl.Domain.Security.Tokens;
using MyExpenseControl.Domain.Services.LoggedUser;
using MyExpenseControl.Infrastructure.DataAccess.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyExpenseControl.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly MyExpenseControlDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(MyExpenseControlDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }

        public async Task<User> User()
        {
            var token = _tokenProvider.Value();
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value;

            var userIdentifier = Guid.Parse(identifier);

            return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
        }
    }
}
