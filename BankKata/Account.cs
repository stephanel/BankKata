using System.Collections.Generic;
using System.Linq;

namespace BankKata
{
    public class Account : IAccount
    {
        private readonly IStatementPrinter _console;
        private readonly ICalendar _calendar;

        private readonly List<Transaction> _transactions;

        const string PrintStatementHeader = "Date || Amount || Balance";

        public Account(IStatementPrinter console, ICalendar calendar)
        {
            _calendar = calendar;
            _console = console;

            _transactions = new List<Transaction>();
        }

        public void Deposit(int amount)
        {
            var deposit = new Transaction(amount, _calendar.GetToday());

            _transactions.Add(deposit);
        }

        public void Withdraw(int amount)
        {
            var withdrawal = new Transaction(-amount, _calendar.GetToday());

            _transactions.Add(withdrawal);
        }

        public void PrintStatement()
        {
            PrintHeader();
            PrintTransactions();
        }

        void PrintHeader()
        {
            _console.PrintLine(PrintStatementHeader);
        }

        void PrintTransactions()
        {
            _transactions.Reverse();

            foreach (var transaction in _transactions)
            {
                var balance = _transactions
                    .Where(x => x.Date <= transaction.Date)
                    .Sum(x => x.Amount);

                _console.PrintLine(
                    $"{transaction.Date.ToShortDateString()} || {transaction.Amount} || {balance}");
            }
        }
    }
}
