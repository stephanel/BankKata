using BankKata;
using System;
using Xunit;

namespace BankKataTests
{
    public class TransactionRepositoryShould
    {
        private readonly TransactionRepository _sut;

        public TransactionRepositoryShould()
        {
            _sut = new TransactionRepository();
        }

        [Fact]
        public void Add_A_transaction()
        {
            // Arrange
            DateTime now = DateTime.Now;

            Transaction transaction = new Transaction(200, now);

            // Act
            _sut.AddTransaction(transaction);

            // Assert
            var actualTransaction = _sut.GetTransactions();

            Assert.Single(actualTransaction);
            Assert.Equal(200, actualTransaction[0].Amount);
            Assert.Equal(now, actualTransaction[0].Date);
        }
    }
}
