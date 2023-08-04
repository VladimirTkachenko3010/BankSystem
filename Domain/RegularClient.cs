using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    /// <summary>
    /// Class for regular clients
    /// </summary>
    public class RegularClient : Client
    {

        public RegularClient(string name, string accountNumber)
            : base(name, accountNumber)
        {

        }

        private const decimal RegularClientMinDepAmount = 1000; // Minimum deposit amount for a regular client
        protected override decimal MinDepAmount => RegularClientMinDepAmount;
        protected override decimal MinCreditAmount => 5000;  //Minimum loan amount


        /// <summary>
        /// Static method to create a RegularClient
        /// </summary>
        /// <returns></returns>
        public static RegularClient CreateRegularClient(string accountNumber)
        {
            //Console.Write("Enter client name: ");
            var name = string.Empty;

            //need to move the excaption to the menu
            try
            {
                name = Console.ReadLine();

                while (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty. Please enter a valid Regular Client name:");
                    name = Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading input. >>{e.Message}  {e.GetType}");
            }

            return new RegularClient(name, accountNumber);
        }


        /// <summary>
        /// deposit method for regular client
        /// </summary>
        /// <param name="amount"></param>
        public override (decimal Balance, string msg) OpenDeposit(decimal amount, string msg)
        {
            base.OpenDeposit(amount, msg);

            // Additional logic for a regular client when opening a deposit
            // Change the amount using random
            Random random = new();
            decimal modifiedAmount = random.Next(-1000, 1000); // Random amount from -1000 to 1000

            msg = $"Обычный клиент: открытие вклада с учетом рандома на сумму: {modifiedAmount} гривен.";
            // Change the balance to modifiedAmount
            Balance += modifiedAmount;

            return (Balance, msg);
        }

        /// <summary>
        /// interest rate for regular client
        /// </summary>
        /// <returns></returns>
        protected override decimal CalcInterestRate()
        {
            // Generating a random interest rate ranging from 1% to 5% for a regular client
            Random random = new();
            decimal interestRate = (decimal)(random.NextDouble() * (5 - 1) + 1);
            return interestRate;
        }

    }
}
