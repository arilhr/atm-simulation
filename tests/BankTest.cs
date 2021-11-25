using atm_simulation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace tests
{
    public class BankTest
    {
        #region Deposit Test
        [Fact]
        public void BankDepositTest()
        {
            Bank bank = new Bank();

            string accountName = "Test1";
            float depositAmount = 100;

            bank.CreateAccount(accountName);
            bank.Deposit(accountName, depositAmount);

            Assert.Equal(depositAmount, bank.GetAccount(accountName).Balance);
        }

        [Fact]
        public void BankDepositWhenOwedTest()
        {
            Bank bank = new Bank();

            string accountName1 = "Test1";
            string accountName2 = "Test2";

            bank.CreateAccount(accountName1);
            bank.CreateAccount(accountName2);
            bank.Transfer(accountName1, accountName2, 100);
            bank.Deposit(accountName1, 110);

            Assert.Equal(10, bank.GetAccount(accountName1).Balance);
        }

        [Fact]
        public void BankDepositAccountNotExistTest()
        {
            Bank bank = new Bank();

            string accountName1 = "Test1";
            bank.Deposit(accountName1, 110);
        }
        #endregion

        #region Withdraw Test

        [Fact]
        public void BankWithdrawSuccessTest()
        {
            Bank bank = new Bank();

            string accountName = "Test1";
            float depositAmount = 100;
            float withdrawAmount = 30;

            bank.CreateAccount(accountName);
            bank.Deposit(accountName, depositAmount);
            bank.Withdraw(accountName, withdrawAmount);

            Assert.Equal(depositAmount - withdrawAmount, bank.GetAccount(accountName).Balance);
        }

        [Fact]
        public void BankWithdrawAccountNotExistTest()
        {
            Bank bank = new Bank();

            string accountName = "Test1";
            float withdrawAmount = 30;

            bank.Withdraw(accountName, withdrawAmount);
        }

        [Fact]
        public void BankWithdrawWhenBalanceInsufficientTest()
        {
            Bank bank = new Bank();

            string accountName = "Test1";
            float depositAmount = 30;
            float withdrawAmount = 100;

            bank.CreateAccount(accountName);
            bank.Deposit(accountName, depositAmount);
            bank.Withdraw(accountName, withdrawAmount);
        }

        #endregion

        #region Transfer Test
        [Fact]
        public void BankTransferSuccessTest()
        {
            Bank bank = new Bank();

            string accountName1 = "Test1";
            string accountName2 = "Test2";

            bank.CreateAccount(accountName1);
            bank.CreateAccount(accountName2);

            bank.Deposit(accountName1, 100);
            bank.Transfer(accountName1, accountName2, 50);

            Assert.Equal(50, bank.GetAccount(accountName1).Balance);
            Assert.Equal(50, bank.GetAccount(accountName2).Balance);
        }

        [Fact]
        public void BankTransferTargetAccountNotFoundTest()
        {
            Bank bank = new Bank();

            string accountName1 = "Test1";
            string accountName2 = "Test2";

            bank.CreateAccount(accountName1);

            bank.Transfer(accountName1, accountName2, 50);
        }

        [Fact]
        public void BankTransferOwedTest()
        {
            Bank bank = new Bank();

            string accountName1 = "Test1";
            string accountName2 = "Test2";

            bank.CreateAccount(accountName1);
            bank.CreateAccount(accountName2);

            bank.Deposit(accountName1, 20);
            bank.Transfer(accountName1, accountName2, 50);

            Assert.Equal(0, bank.GetAccount(accountName1).Balance);
            Assert.Equal(20, bank.GetAccount(accountName2).Balance);
        }

        [Fact]
        public void BankTransferWhenOwedToTargetAccountTest()
        {
            Bank bank = new Bank();

            string accountName1 = "Test1";
            string accountName2 = "Test2";

            bank.CreateAccount(accountName1);
            bank.CreateAccount(accountName2);

            bank.GetAccount(accountName1).Balance = 100;
            bank.GetAccount(accountName1).AddOwedList(bank.GetAccount(accountName2), 20);

            bank.Transfer(accountName1, accountName2, 20);

            Assert.False(bank.GetAccount(accountName1).IsAccountOwed());
        }
        #endregion
    }
}
