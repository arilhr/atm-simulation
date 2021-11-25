using System;
using System.Collections.Generic;
using System.Text;
using atm_simulation;
using Xunit;

namespace tests
{
    public class AccountTest
    {
        #region Owed Test

        [Fact]
        public void AccountAddOwedListTest()
        {
            Account accountOwed = new Account("Test1", 100);
            Account accountToOwed = new Account("Test2", 0);

            accountOwed.AddOwedList(accountToOwed, 20);
            accountOwed.AddOwedList(accountToOwed, 20);

            Assert.Equal(40, accountOwed.GetTotalOwedToAccount(accountToOwed));
        }

        [Fact]
        public void AccountPayOwedTest()
        {
            Account accountOwed = new Account("Test1", 10);
            Account accountToOwed = new Account("Test2", 0);

            accountOwed.AddOwedList(accountToOwed, 20);
            accountOwed.PayAccountOwed(accountOwed.Balance);

            Assert.Equal(10, accountOwed.GetTotalOwedToAccount(accountToOwed));
        }

        #endregion
    }
}
