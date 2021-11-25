using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using atm_simulation;

namespace tests
{
    public class UnitTest
    {

        [Fact]
        public void ATMReadInputTest()
        {
            Bank bank = new Bank();
            ATM atm = new ATM(bank);

            string accountName = "Test";
            string account2Name = "Test2";
            bank.CreateAccount(account2Name);

            // test false input
            atm.ReadInputUser($"sign in {accountName}");

            // test input login
            atm.ReadInputUser($"login {accountName}");
            Assert.NotNull(atm.GetCurrentAccount());

            // test input deposit
            atm.ReadInputUser($"deposit 100");
            Assert.Equal(100, atm.GetCurrentAccount().Balance);

            // test input withdraw
            atm.ReadInputUser($"withdraw 20");
            Assert.Equal(80, atm.GetCurrentAccount().Balance);

            // test input transfer
            atm.ReadInputUser($"transfer {account2Name} 30");
            Assert.Equal(50, atm.GetCurrentAccount().Balance);
            Assert.Equal(30, bank.GetAccount(account2Name).Balance);

            // test input logout
            atm.ReadInputUser($"logout");
            Assert.Null(atm.GetCurrentAccount());
        }

        [Fact]
        public void LoginTest()
        {
            ATM atm = new ATM();

            string accountName = "Test";
            atm.LoginAccount(accountName);

            Assert.NotNull(atm.GetCurrentAccount());
        }

        [Fact]
        public void LoginWhenAnotherAccountLoginTest()
        {
            ATM atm = new ATM();

            string accountName = "Test";
            string accountName2 = "Test2";
            atm.LoginAccount(accountName);
            Account account1 = atm.GetCurrentAccount();
            atm.LoginAccount(accountName2);
            
            Assert.Equal(account1, atm.GetCurrentAccount());
        }

        [Fact]
        public void DepositTest()
        {
            ATM atm = new ATM();

            atm.LoginAccount("Test");
            atm.Deposit(100);

            Assert.Equal(100, atm.GetCurrentAccount().Balance);
        }

        [Fact]
        public void DepositWhenUserNotLoginTest()
        {
            ATM atm = new ATM();

            atm.Deposit(100);
        }

        [Fact]
        public void DepositWhenUserHasOwedTest()
        {
            Bank bank = new Bank();
            ATM atm = new ATM(bank);

            string accountName1 = "Test1";
            string accountName2 = "Test2";

            bank.CreateAccount(accountName1);
            bank.CreateAccount(accountName2);

            atm.LoginAccount(accountName1);
            atm.Transfer(accountName2, 20);
            atm.Deposit(30);

            Assert.Equal(10, atm.GetCurrentAccount().Balance);
        }

        [Fact]
        public void WithdrawTest()
        {
            ATM atm = new ATM();

            atm.LoginAccount("Test");
            atm.Deposit(100);
            atm.Withdraw(100);

            Assert.Equal(0, atm.GetCurrentAccount().Balance);
        }

        [Fact]
        public void WithdrawWhenUserNotLoginTest()
        {
            ATM atm = new ATM();

            atm.Withdraw(100);
        }

        [Fact]
        public void TransferSuccessTest()
        {
            Bank bank = new Bank();
            ATM atm = new ATM(bank);

            bank.CreateAccount("Test1");
            bank.CreateAccount("Test2");

            atm.LoginAccount("Test1");
            atm.Deposit(100);
            atm.Transfer("Test2", 20);

            Assert.Equal(80, atm.GetCurrentAccount().Balance);
            Assert.Equal(20, bank.GetAccount("Test2").Balance);
        }

        [Fact]
        public void TransferOwedTest()
        {
            Bank bank = new Bank();
            ATM atm = new ATM(bank);

            bank.CreateAccount("Test1");
            bank.CreateAccount("Test2");

            atm.LoginAccount("Test1");
            atm.Transfer("Test2", 10);
        }
    }
}
