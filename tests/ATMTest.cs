using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using atm_simulation;

namespace tests
{
    public class ATMTest
    {
        #region Login Test
        [Fact]
        public void ATMInputLoginTest()
        {
            ATM atm = new ATM();
            atm.ReadInputUser("login testAccount");

            Assert.NotNull(atm.GetCurrentAccount());
        }

        [Fact]
        public void ATMFalseInputLoginTest()
        {
            ATM atm = new ATM();
            atm.ReadInputUser("login testAccount 100");

            Assert.Null(atm.GetCurrentAccount());
        }

        [Fact]
        public void ATMLoginWhenAnotherAccountStillLoginTest()
        {
            ATM atm = new ATM();

            atm.LoginAccount("Test1");
            atm.LoginAccount("Test2");
            
            Assert.Equal("Test1", atm.GetCurrentAccount().Name);
        }
        #endregion

        #region Logout Test
        [Fact]
        public void ATMLogoutTest()
        {
            ATM atm = new ATM();

            atm.ReadInputUser("login testAccount");
            atm.ReadInputUser("logout");

            Assert.Null(atm.GetCurrentAccount());
        }

        [Fact]
        public void ATMLogoutWhenNotLogggedInTest()
        {
            ATM atm = new ATM();

            atm.ReadInputUser("logout");

            Assert.Null(atm.GetCurrentAccount());
        }


        #endregion

        #region Deposit Test
        [Fact]
        public void ATMInputDepositTest()
        {
            ATM atm = new ATM();

            atm.ReadInputUser("login testAccount");
            atm.ReadInputUser("deposit 100");

            Assert.Equal(100, atm.GetCurrentAccount().Balance);
        }

        [Fact]
        public void ATMDepositWhenUserNotLoginTest()
        {
            ATM atm = new ATM();

            atm.ReadInputUser("deposit 100");
        }
        #endregion

        #region Transfer Test

        [Fact]
        public void ATMTransferTest()
        {
            ATM atm = new ATM();

            atm.ReadInputUser("login testAccount");
            atm.ReadInputUser("transfer test2 200");
        }

        [Fact]
        public void TransferWhenUserNotLoginTest()
        {
            ATM atm = new ATM();

            atm.ReadInputUser("transfer test2 200");
        }

        #endregion

        #region Withdraw Test
        [Fact]
        public void ATMWithdrawTest()
        {
            ATM atm = new ATM();

            atm.ReadInputUser("login testAccount");
            atm.ReadInputUser("withdraw 100");
        }

        [Fact]
        public void ATMWithdrawWhenUserNotLoginTest()
        {
            ATM atm = new ATM();

            atm.ReadInputUser("withdraw 100");
        }
        #endregion

    }
}
