using Moq;
using MyExpenseControl.Domain.Repositories;


namespace CommonTestUtilities.Repository
{
    public class UnitOfWorkBuilder
    {
        //A função IUnitOfWork não devolve nada, mesma coisa do
        //WriteOnlye não devolve nada
        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();

            //Implementação fake
            return mock.Object;
        }
    }
}
