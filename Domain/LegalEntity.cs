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
        public override decimal MinDepAmount => LegalEntityMinDep;
        public override decimal MinCreditAmount => 50000;  //Minimum loan amount


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
        public override decimal OpenDeposit(Client depositClient, decimal amount, StringBuilder message)
        {
            //var (_, message) = base.OpenDeposit(depositClient, amount, message);
            base.OpenDeposit(depositClient, amount, message);

            //Additional logic for opening a deposit for legal entities
            // Changing the amount using random
            var random = new Random();
            decimal randomFactor = random.Next(40, 61); // Random factor
            var modifiedAmount = amount * randomFactor / 100;
            message.Append($"\nLegal entity: changing deposit amount based on random factor: {randomFactor}%.\nLegal entity: modified deposit amount: {amount + modifiedAmount} UAH.");

            // Change the balance to modifiedAmount
            Balance += modifiedAmount;
            return Balance;
        }

        ///// <summary>
        ///// Interest rate for legal entities
        ///// </summary>
        ///// <returns></returns>
        //protected override decimal CalcInterestRate()
        //{
        //    var random = new Random();
        //    var interestRate = (decimal)(random.NextDouble() * (7 - 3) + 1);
        //    return interestRate;
        //}

    }
}