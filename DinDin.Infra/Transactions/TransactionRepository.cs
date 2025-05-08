using DinDin.Domain.Transactions;
using Raven.Client.Documents.Session;

namespace DinDin.Infra.Transactions
{
    public class TransactionRepository(IAsyncDocumentSession session) : ITransactionRepository
    {
        private readonly IAsyncDocumentSession _session = session;

        public async Task Add(Transaction transaction)
        {
            await _session.StoreAsync(transaction);
            await _session.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetAll(FilterTransaction? filter = null)
        {
            throw new NotImplementedException();
        }

        public Transaction GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}