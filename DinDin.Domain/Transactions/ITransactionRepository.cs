namespace DinDin.Domain.Transactions
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll(FilterTransaction? filter = null);
        Transaction GetById(int id);
        void Add(Transaction transaction);
        void Update (Transaction transaction);
        void Delete (int id);
    }
}