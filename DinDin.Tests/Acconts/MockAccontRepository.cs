using DinDin.Domain.Acconts;
using DinDin.Infra.Acconts;

namespace DinDin.Tests.Acconts
{
    public class MockAccontRepository : IAccontRepository
    {
        private readonly AccontSingleton _instance;

        public MockAccontRepository()
        {
            _instance = AccontSingleton.Instance;
        }

        public void Add(Accont accont)
        {
            _instance.Add(accont);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Accont GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Accont Update(Accont accont)
        {
            throw new NotImplementedException();
        }
    }
}