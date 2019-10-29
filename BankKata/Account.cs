using System.Collections.Generic;
using System.Linq;

namespace BankKata
{
    public class Account : IAccount
    {
        private readonly IStatementPrinter _console;
        private readonly ICalendar _calendar;
        private readonly ITransactionRepository _accountRepository;

        const string PrintStatementHeader = "Date || Amount || Balance";

        public Account(IStatementPrinter console, ICalendar calendar, ITransactionRepository accountRepository)
        {
            _calendar = calendar;
            _console = console;
            _accountRepository = accountRepository;
        }

        public void Deposit(int amount)
        {
            var deposit = new Transaction(amount, _calendar.GetNow());

            _accountRepository.AddTransaction(deposit);
        }

        public void Withdraw(int amount)
        {
            var withdrawal = new Transaction(-amount, _calendar.GetNow());

            _accountRepository.AddTransaction(withdrawal);
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
            var transactions = _accountRepository.GetTransactions();

            transactions.Reverse();

            foreach (var transaction in transactions)
            {
                var balance = transactions
                    .Where(x => x.Date <= transaction.Date)
                    .Sum(x => x.Amount);

                _console.PrintLine(
                    $"{transaction.Date.ToShortDateString()} || {transaction.Amount} || {balance}");
            }
        }
    }
}
