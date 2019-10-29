using System.Collections.Generic;

namespace BankKata
{
    public interface ITransactionRepository
    {
        void AddTransaction(Transaction transaction);
        List<Transaction> GetTransactions();
    }
}
