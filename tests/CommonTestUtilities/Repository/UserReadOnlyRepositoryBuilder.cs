using Moq;
using MyExpenseControl.Domain.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repository
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }

        public void ExistActiveUserWithEmail(string email)
        {
            //Sempre voltar true, não importa o email
            //_repository.Setup(repository => repository.ExistActiveUserWithEmail(It.IsAny<string>()));

            _repository.Setup(repository => repository.ExisteActiveUserWithEmail(email)).ReturnsAsync(true);
        }
    }
}
