using System;
using System.Collections.Generic;
using System.Text;

namespace atm_simulation
{
    public class ATM
    {
        private Bank bank;
        private Account currentLoginAccount = null;

        public ATM()
        {
            bank = new Bank();
        }

        public ATM(Bank bank)
        {
            this.bank = bank;
        }

        public void ReadInputUser(string input)
        {
            string[] inputSplit = input.Split(" ");
            switch (inputSplit[0])
            {
                case "login":
                    if (inputSplit.Length == 2)
                    {
                        LoginAccount(inputSplit[1]);
                    }
                    break;
                case "deposit":
                    if (inputSplit.Length == 2)
                    {
                        float amountToDeposit = float.Parse(inputSplit[1]);
                        Deposit(amountToDeposit);
                    }
                    break;
                case "transfer":
                    if (inputSplit.Length == 3)
                    {
                        Transfer(inputSplit[1], float.Parse(inputSplit[2]));
                    }
                    break;
                case "withdraw":
                    if (inputSplit.Length == 2)
                    {
                        Withdraw(float.Parse(inputSplit[1]));
                    }
                    break;
                case "logout":
                    LogoutAccount();
                    break;
                default:
                    break;
            }

            Console.WriteLine();
        }

        public void LoginAccount(string name)
        {
            // check if still logged in on another account
            if (currentLoginAccount != null)
            {
                Console.WriteLine($"You still logged in on: {currentLoginAccount.Name}. Logout from this account first.");
                return;
            }

            // check if account is doesn't exist, create account
            if (!bank.CheckIfAccountExist(name))
            {
                bank.CreateAccount(name);
            }

            // login account
            currentLoginAccount = bank.GetAccount(name);

            // hello
            Console.WriteLine($"Hello, {currentLoginAccount.Name}!");
            Console.WriteLine($"Your balance is ${currentLoginAccount.Balance}");
            currentLoginAccount.ShowAccountOwedList();
        }

        public void Deposit(float amount)
        {
            if (currentLoginAccount == null)
            {
                Console.WriteLine($"Deposit Failed: You not login on any account.");
                return;
            }

            bank.Deposit(currentLoginAccount.Name, amount);
        }

        public void Withdraw(float amount)
        {
            if (currentLoginAccount == null)
            {
                Console.WriteLine($"Transfer Failed: You not login on any account.");
                return;
            }

            bank.Withdraw(currentLoginAccount.Name, amount);
        }

        public void Transfer(string targetName, float amount)
        {
            if (currentLoginAccount == null)
            {
                Console.WriteLine($"Transfer Failed: You not login on any account.");
                return;
            }

            bank.Transfer(currentLoginAccount.Name, targetName, amount);
        }

        public void LogoutAccount()
        {
            if (currentLoginAccount == null)
            {
                Console.WriteLine($"You not login on any account.");
                return;
            }

            Console.WriteLine($"Goodbye, {currentLoginAccount.Name}");
            currentLoginAccount = null;
        }

        public Account GetCurrentAccount()
        {
            return currentLoginAccount;
        }
    }
}
