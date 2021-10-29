using atm_simulation;
using Xunit;

namespace tests
{
    public class ATMUnitTest
    {
        [Fact]
        public void LoginAccountTest()
        {
            ATM atm = new ATM();

            atm.LoginAccount("Test");

            Assert.NotNull(atm.GetCurrentAccount());
        }

        [Fact]
        public void ATMDepositTest()
        {
            ATM atm = new ATM();

            atm.LoginAccount("Test");
            atm.Deposit(100);

            Assert.Equal(100, atm.GetCurrentAccount().Balance);
        }

        [Fact]
        public void ATMTransferTest()
        {
            Bank bank = new Bank();
            ATM atm = new ATM(bank);

            bank.CreateAccount("Test");
            bank.CreateAccount("Test2");

            atm.LoginAccount("Test");
            atm.Deposit(50);

            atm.Transfer("Test2", 20);

            Assert.Equal(20, bank.GetAccount("Test2").Balance);
            Assert.Equal(30, bank.GetAccount("Test").Balance);
        }

        [Fact]
        public void ATMWithdrawTest()
        {
            ATM atm = new ATM();

            atm.LoginAccount("Test");
            atm.Deposit(100);
            atm.Withdraw(30);

            Assert.Equal(70, atm.GetCurrentAccount().Balance);
        }

        [Fact]
        public void ATMLogoutTest()
        {
            ATM atm = new ATM();

            atm.LoginAccount("Test");
            atm.LogoutAccount();

            Assert.Null(atm.GetCurrentAccount());
        }
    }
}
