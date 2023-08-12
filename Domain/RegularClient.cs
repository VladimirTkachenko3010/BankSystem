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
        public override decimal MinDepAmount => RegularClientMinDepAmount;
        public override decimal MinCreditAmount => 5000;  //Minimum loan amount


        /// <summary>
        /// Static method to create a RegularClient
        /// </summary>
        /// <returns></returns>
        public static RegularClient CreateRegularClient(string name, string accountNumber)
        {
            return new RegularClient(name, accountNumber);
        }


        /// <summary>
        /// deposit method for regular client
        /// </summary>
        /// <param name="amount"></param>
        public override decimal OpenDeposit(Client depositClient, decimal amount, StringBuilder message)
        {
            base.OpenDeposit(depositClient, amount, message);

            // Additional logic for a regular client when opening a deposit
            // Change the amount using random
            Random random = new Random();
            decimal modifiedAmount = random.Next(-1000, 1000); // Random amount from -1000 to 1000

            message.Append($"\nRegular client: opening a deposit, taking into account the random amount for the amount: {modifiedAmount}%.\n");
            // Change the balance to modifiedAmount
            Balance += modifiedAmount;

            return Balance;
        }

        /// <summary>
        /// interest rate for regular client
        /// </summary>
        /// <returns></returns>
        protected override decimal CalcInterestRate()
        {
            // Generating a random interest rate ranging from 1% to 5% for a regular client
            Random random = new Random();
            decimal interestRate = (decimal)(random.NextDouble() * (5 - 1) + 1);
            return interestRate;
        }

    }
}