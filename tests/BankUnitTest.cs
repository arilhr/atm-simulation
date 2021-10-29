using System;
using Xunit;
using atm_simulation;

namespace tests
{
    public class BankUnitTest
    {
        [Fact]
        public void CreateAccounTest()
        {
            Bank bank = new Bank();

            string accountName = "Test";
            bank.CreateAccount(accountName);

            Assert.True(bank.CheckIfAccountExist(accountName));
        }

        [Fact]
        public void DepositTest()
        {
            Bank bank = new Bank();
            string accountName = "Test";
            bank.CreateAccount(accountName);
            Account createdAccount = bank.GetAccount(accountName);

            float depositTestAmount = 100;
            bank.Deposit(createdAccount, depositTestAmount);

            Assert.Equal(depositTestAmount, createdAccount.Balance);
        }

        [Fact]
        public void TransferTest()
        {
            Bank bank = new Bank();
            bank.CreateAccount("Test1");
            bank.CreateAccount("Test2");
            Account firstAccount = bank.GetAccount("Test1");
            Account secondAccount = bank.GetAccount("Test2");

            bank.Deposit(firstAccount, 100);
            bank.Transfer(firstAccount, secondAccount, 30);

            Assert.Equal(70, firstAccount.Balance);
            Assert.Equal(30, secondAccount.Balance);
        }

        [Fact]
        public void WithdrawTest()
        {
            Bank bank = new Bank();
            bank.CreateAccount("Test1");
            Account testAccount = bank.GetAccount("Test1");

            bank.Deposit(testAccount, 100);
            bank.Withdraw(testAccount, 30);

            Assert.Equal(70, testAccount.Balance);
        }
    }

    
}
