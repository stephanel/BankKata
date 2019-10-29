using System.Collections.Generic;
using System.Linq;

namespace BankKata
{
    public static class ListOfTransactionsExtensions
    {
        public static List<string> GetFormattedTransactionsOuputs(this List<Transaction> transactions)
        {
            List<string> ouputs = new List<string>();

            transactions.Reverse();

            foreach (var transaction in transactions)
            {
                var balance = transactions
                    .Where(x => x.Date <= transaction.Date)
                    .Sum(x => x.Amount);

                ouputs.Add($"{transaction.Date.ToShortDateString()} || {transaction.Amount} || {balance}");
            }

            return ouputs;
        }
    }
}
