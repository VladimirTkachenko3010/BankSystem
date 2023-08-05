using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    /// <summary>
    /// Class for legal entities
    /// </summary>
    public class LegalEntity : Client
    {
        public LegalEntity(string name, string accountNumber)
            : base(name, accountNumber)
        {
        }

        private const decimal LegalEntityMinDep = 10000; // Minimum deposit amount for a legal entity
        protected override decimal MinDepAmount => LegalEntityMinDep;
        protected override decimal MinCreditAmount => 50000;  //Minimum loan amount


        /// <summary>
        /// method to create client LegalEntity
        /// </summary>
        /// <returns></returns>
        public static LegalEntity CreateLegalEntity(string name, string accountNumber)
        {
            return new LegalEntity(name, accountNumber);
        }

        /// <summary>
        /// deposit method for legal entities
        /// </summary>
        /// <param name="amount"></param>
        public override (decimal Balance, string msg) OpenDeposit(decimal amount, string msg)
        {
            var (_, message) = base.OpenDeposit(amount, msg);
            //Additional logic for opening a deposit for legal entities
            // Changing the amount using random
            Random random = new();
            decimal randomFactor = random.Next(40, 61); // Random factor
            decimal modifiedAmount = amount * randomFactor / 100;
            message += $"\nLegal entity: changing deposit amount based on random factor: {randomFactor}%.";
            message += $"\nLegal entity: modified deposit amount: {amount + modifiedAmount} UAH.";

            // Change the balance to modifiedAmount
            Balance += modifiedAmount;
            return (Balance, message);
        }

        /// <summary>
        /// Interest rate for legal entities
        /// </summary>
        /// <returns></returns>
        protected override decimal CalcInterestRate()
        {
            // Generating a random interest rate in the range from 3% to 7%
            Random random = new();
            decimal interestRate = (decimal)(random.NextDouble() * (7 - 3) + 1);
            return interestRate;
        }

    }
}
