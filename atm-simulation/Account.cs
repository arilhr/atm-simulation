using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace atm_simulation
{
    public class Account
    {
        private string name;
        private float balance;
        private Dictionary<Account, float> accountOwed = new Dictionary<Account, float>();

        #region Constructor
        public Account() { }
        public Account(string name, float balance)
        {
            this.name = name;
            this.balance = balance;
        }
        #endregion

        #region Name
        public string Name
        {
            get => name;
        }

        #endregion

        #region Balance
        public float Balance
        {
            get => balance;
            set
            {
                balance = value;
            }
        }
        #endregion

        #region Owed List Function
        public bool IsAccountOwed()
        {
            return accountOwed.Count > 0;
        }

        public bool IsAccountOwedToTargetAccount(Account targetAccount)
        {
            return accountOwed.ContainsKey(targetAccount);
        }

        public void AddOwedList(Account acc, float amount)
        {
            if (accountOwed.ContainsKey(acc))
            {
                accountOwed[acc] += amount;
            }
            else
            {
                accountOwed.Add(acc, amount);
            }
        }

        // get total account owed
        public float GetTotalOwedToAccount(Account acc)
        {
            if (!accountOwed.ContainsKey(acc)) return 0;

            return accountOwed[acc];
        }

        // show all owed account list
        public void ShowAccountOwedList()
        {
            for (int i = 0; i < accountOwed.Count; i++)
            {
                Console.WriteLine($"Owed ${accountOwed.ElementAt(i).Value} to {accountOwed.ElementAt(0).Key.name}");
            }
        }

        // pay owed on specific account
        public void PayOwedSpecificAccount(Account targetAccount, float amount)
        {
            if (!accountOwed.ContainsKey(targetAccount)) return;

            accountOwed[targetAccount] -= amount;
            targetAccount.balance += amount;

            if (accountOwed[targetAccount] <= 0)
                accountOwed.Remove(targetAccount);
        }

        // pay all account owed on list
        public float PayAccountOwed(float amount)
        {
            float amountLeft = amount;

            while (IsAccountOwed())
            {
                Account accountToPay = accountOwed.ElementAt(0).Key;
                if (accountOwed.ElementAt(0).Value > amountLeft)
                {
                    accountToPay.balance += amountLeft;
                    accountOwed[accountToPay] -= amountLeft;
                    Console.WriteLine($"Transferred ${amountLeft} to {accountToPay.name}");

                    if (accountOwed[accountToPay] <= 0) accountOwed.Remove(accountToPay);

                    amountLeft = 0;
                    break;
                }
                else
                {
                    accountToPay.balance += accountOwed[accountToPay];
                    amountLeft -= accountOwed[accountToPay];

                    Console.WriteLine($"Transferred ${accountOwed[accountToPay]} to {accountToPay.name}");

                    accountOwed.Remove(accountToPay);
                }
            }

            ShowAccountOwedList();
            return amountLeft;
        }
        #endregion
    }
}
