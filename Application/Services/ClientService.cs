using Application.Services.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        public decimal CalcInterestRate(Client client)
        {
            var random = new Random();
            var interestRate = default(decimal);
            switch (client)
            {
                case VIPClient:
                    // Generating a random interest rate in the range from 5% to 10%
                    interestRate = (decimal)(random.NextDouble() * (10 - 5) + 1);
                    break;

                case LegalEntity:
                    // Generating a random interest rate in the range from 3% to 7%
                    interestRate = (decimal)(random.NextDouble() * (7 - 3) + 1);
                    break;

                case RegularClient:
                    // Generating a random interest rate ranging from 1% to 5% for a regular client
                    interestRate = (decimal)(random.NextDouble() * (5 - 1) + 1);
                    break;
            }
            return interestRate;
        }

        /// <summary>
        /// client service dep
        /// </summary>
        /// <param name="depositClient"></param>
        /// <param name="amount"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public decimal OpenDeposit(Client depositClient, decimal amount, StringBuilder message)
        {
            var random = new Random();
            var modifiedAmount = default(decimal);
            switch (depositClient)
            {
                case VIPClient:
                    var randomFactor = CalcInterestRate(depositClient); // Random factor
                    modifiedAmount = amount * randomFactor / 100;
                    depositClient.Balance += amount;
                    break;

                case LegalEntity:
                    break;

                case RegularClient:
                    break;
            }    

            return modifiedAmount;
        }
    }
}
