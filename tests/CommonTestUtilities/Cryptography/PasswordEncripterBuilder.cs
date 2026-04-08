using MyExpenseControl.Domain.Security.Cryptography;
using MyExpenseControl.Infrastructure.Security.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new Sha512Encripter("ABCTeste");
    }
}
