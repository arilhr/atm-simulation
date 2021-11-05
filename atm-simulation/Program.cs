using System;

namespace atm_simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM();

            while (true)
            {
                string input = Console.ReadLine();
                atm.ReadInputUser(input);
            }

        }
    }
}
