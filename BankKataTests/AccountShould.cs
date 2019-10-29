using BankKata;
using Moq;
using System;
using Xunit;

namespace BankKataTests
{
    public class AccountShould
    {
        private readonly Account _sut;

        private readonly Mock<IStatementPrinter> mockConsole;
        private readonly Mock<ICalendar> mockCalendar;
        private readonly Mock<ITransactionRepository> mockTransactionRepository;

        private readonly DateTime _now = DateTime.Now;


        public AccountShould()
        {
            mockConsole = new Mock<IStatementPrinter>();
            mockCalendar = new Mock<ICalendar>();
            mockTransactionRepository = new Mock<ITransactionRepository>();

            _sut = new Account(mockConsole.Object, mockCalendar.Object, mockTransactionRepository.Object);

            mockCalendar.Setup(_ => _.GetNow()).Returns(_now);
        }

        [Fact]
        public void Add_A_Deposit()
        {
            // Arrange
            // Act
            _sut.Deposit(200);

            // Assert
            mockTransactionRepository.Verify(_ => _.AddTransaction(
                It.Is<Transaction>(transaction =>  transaction.Amount == 200 && transaction.Date == _now)),
                Times.Once);
        }

        [Fact]
        public void Add_A_Withdrawal()
        {
            // Arrange
            // Act
            _sut.Withdraw(200);

            // Assert
            mockTransactionRepository.Verify(_ => _.AddTransaction(
                It.Is<Transaction>(transaction => transaction.Amount == -200 && transaction.Date == _now)),
                Times.Once);
        }
    }
}
