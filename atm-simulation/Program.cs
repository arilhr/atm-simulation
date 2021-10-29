using System;

namespace atm_simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            ATM atm = new ATM(bank);

            while (true)
            {
                string input = Console.ReadLine();
                string[] inputSplit = input.Split(" ");
                switch (inputSplit[0])
                {
                    case "login":
                        atm.LoginAccount(inputSplit[1]);
                        break;
                    case "deposit":
                        float amountToDeposit = float.Parse(inputSplit[1]);
                        atm.Deposit(amountToDeposit);
                        break;
                    case "transfer":
                        atm.Transfer(inputSplit[1], float.Parse(inputSplit[2]));
                        break;
                    case "withdraw":
                        atm.Withdraw(float.Parse(inputSplit[1]));
                        break;
                    case "logout":
                        atm.LogoutAccount();
                        break;
                    default:
                        break;
                }

                Console.WriteLine();
            }

        }
    }
}
