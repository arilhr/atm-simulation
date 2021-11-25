using System;
using System.Collections.Generic;
using System.Text;

namespace atm_simulation
{
    public class Bank
    {
        private List<Account> accounts;

        public Bank()
        {
            accounts = new List<Account>();
        }

        public bool CheckIfAccountExist(string accountName)
        {
            return accounts.Exists(x => x.Name == accountName);
        }

        public Account GetAccount(string accountName)
        {
            return accounts.Find(x => x.Name == accountName);
        }

        public void CreateAccount(string name)
        {
            if (accounts.Exists(x => x.Name == name)) return;

            // create new account
            Account newAccount = new Account(name, 0);
            // add new account to list of account
            accounts.Add(newAccount);
        }

        public void Deposit(string accountName, float amount)
        {
            Account account = GetAccount(accountName);
            float amountDeposit = amount;
            
            if (account == null)
            {
                Console.WriteLine($"Deposit Failed: Account not exist.");
                return;
            }

            // check if account has owed, pay the owed first
            if (account.IsAccountOwed())
            {
                amountDeposit = account.PayAccountOwed(amountDeposit);
            }

            // add account balance
            account.Balance += amountDeposit;

            // show current account balance and owed list
            Console.WriteLine($"Your balance is ${account.Balance}");
            account.ShowAccountOwedList();
        }

        public void Withdraw(string accountName, float amount)
        {
            Account account = GetAccount(accountName);

            if (account == null)
            {
                Console.WriteLine($"Withdraw Failed: Account not exist.");
                return;
            }

            // check if balance less than request amount withdraw, withdraw failed
            if (account.Balance - amount < 0)
            {
                Console.WriteLine($"Withdraw Failed: You dont have enough balance.");
                return;
            }

            // substract account balance
            account.Balance -= amount;
            Console.WriteLine($"You withdraw ${amount} from your balance.");
        }

        public void Transfer(string accountName, string targetName, float amount)
        {
            Account account = GetAccount(accountName);
            Account targetAccount = GetAccount(targetName);

            // check if target account is exist
            if (targetAccount == null)
            {
                Console.WriteLine($"Transfer Failed: Cant find account with name {targetName}");
                return;
            }

            // if account has owed to target account, pay the owed list
            if (account.IsAccountOwedToTargetAccount(targetAccount))
            {
                account.PayOwedSpecificAccount(targetAccount, account.GetTotalOwedToAccount(targetAccount));
            }

            // if not enough balance, transfer all balance on this account, and add owed with target account
            // else, just transfer normally
            if (account.Balance - amount < 0)
            {
                float owed = amount - account.Balance;
                float transferredAmount = account.Balance;
                account.Balance = 0;

                targetAccount.Balance += transferredAmount;
                Console.WriteLine($"Transferred ${transferredAmount} to {targetAccount.Name}");
                Console.WriteLine($"Your balance is ${account.Balance}");

                account.AddOwedList(targetAccount, owed);
                Console.WriteLine($"Owed ${account.GetTotalOwedToAccount(targetAccount)} to {targetAccount.Name}");
            }
            else
            {
                account.Balance -= amount;

                targetAccount.Balance += amount;
                Console.WriteLine($"Transferred ${amount} to {targetAccount.Name}");
                Console.WriteLine($"Your balance is ${account.Balance}");
            }
        }
    }
}
