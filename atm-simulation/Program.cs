using System;

namespace atm_simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ATM Simulation";

            ATM atm = new ATM();

            Console.WriteLine($"ATM SIMULATION");
            Console.WriteLine($"=========================================\n");
            Console.WriteLine($"login [name]");
            Console.WriteLine($"transfer [target name] [amount]");
            Console.WriteLine($"deposit [name]");
            Console.WriteLine($"logout [name]");
            Console.WriteLine($"\n=========================================\n");

            while (true)
            {
                string input = Console.ReadLine();
                atm.ReadInputUser(input);
            }

        }
    }
}
