using System.Collections.Generic;

namespace BankKata
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }
    }
}
