using BankKata;
using Moq;
using System;
using Xunit;

namespace BankKataTests
{
    public class PrintStatementFeature
    {
        [Fact]
        public void PrintStatement()
        {
            //Date || Amount || Balance
            //145/01/2012 || -2000 || 1500
            //14/01/2012 || 1000 || 3500
            //14/01/2012 || -500 || 2500
            //13/01/2012 || 2000 || 3000
            //10/01/2012 || 1000 || 1000

            // Given
            Mock<ICalendar> calendarMock = new Mock<ICalendar>();
            Mock<IStatementPrinter> consoleMock = new Mock<IStatementPrinter>();

            IAccount account = new Account(consoleMock.Object, calendarMock.Object);

            var calendarSetupSequence = calendarMock
                .SetupSequence(_ => _.GetToday());

            calendarSetupSequence.Returns(new DateTime(2012, 1, 10));
            account.Deposit(1000);

            calendarSetupSequence.Returns(new DateTime(2012, 1, 13));
            account.Deposit(2000);

            calendarSetupSequence.Returns(new DateTime(2012, 1, 14, 0, 0, 0));
            account.Withdraw(500);

            calendarSetupSequence.Returns(new DateTime(2012, 1, 14, 0, 0, 1));
            account.Deposit(1000);

            calendarSetupSequence.Returns(new DateTime(2012, 1, 15));
            account.Withdraw(2000);

            // When
            account.PrintStatement();

            // Then
            consoleMock.Verify(_ => _.PrintLine("Date || Amount || Balance"));
            consoleMock.Verify(_ => _.PrintLine("15/01/2012 || -2000 || 1500"));
            consoleMock.Verify(_ => _.PrintLine("14/01/2012 || 1000 || 3500"));
            consoleMock.Verify(_ => _.PrintLine("14/01/2012 || -500 || 2500"));
            consoleMock.Verify(_ => _.PrintLine("13/01/2012 || 2000 || 3000"));
            consoleMock.Verify(_ => _.PrintLine("10/01/2012 || 1000 || 1000"));
        }
    }
}
