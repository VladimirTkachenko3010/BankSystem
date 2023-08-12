﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    /// <summary>
    /// VIP Client class
    /// </summary>
    public class VIPClient : Client
    {

        public VIPClient(string name, string accountNumber)
             : base(name, accountNumber)
        {
        }

        private const decimal VipClientMinDep = 5000; // Minimum deposit amount for a VIP client
        public override decimal MinDepAmount => VipClientMinDep;
        public override decimal MinCreditAmount => 10000;  //Minimum loan amount


        /// <summary>
        /// method to create a VIPClient
        /// </summary>
        /// <returns></returns>
        public static VIPClient CreateVIPClient(string name, string accountNumber)
        {
            return new VIPClient(name, accountNumber);
        }


        /// <summary>
        /// deposit method for VIP clients
        /// </summary>
        /// <param name="amount"></param>
        public override decimal OpenDeposit(Client depositClient, decimal amount, StringBuilder message)
        {
            base.OpenDeposit(depositClient, amount, message);
            message.Append($"\nVIP client: opening a deposit in the amount of {amount}%.\n");
            return amount;
        }

        /// <summary>
        /// interest rate for VIP
        /// </summary>
        /// <returns></returns>
        protected override decimal CalcInterestRate()
        {
            // Generating a random interest rate in the range from 5% to 10%
            Random random = new Random();
            decimal interestRate = (decimal)(random.NextDouble() * (10 - 5) + 1);
            return interestRate;
        }

    }
}