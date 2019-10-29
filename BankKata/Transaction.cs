using System;

namespace BankKata
{
    public class Transaction
    {
        public Transaction(int amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }

        public int Amount { get; private set; }
        public DateTime Date { get; private set; }
    }
}