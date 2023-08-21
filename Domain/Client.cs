using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// public abstract class for clients
    /// </summary>
    public abstract class Client
    {
        /// <summary>
        /// client name
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// client account number
        /// </summary>
        public string AccountNumber { get; private set; } = null!;
        /// <summary>
        /// client balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// minimum deposit amount for client
        /// </summary>
        public abstract decimal MinDepAmount { get; }
        /// <summary>
        /// minimum credit amount for client
        /// </summary>
        public abstract decimal MinCreditAmount { get; }

        public Client(string name, string accountNumber)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = 0;
        }


        /// <summary>
        /// virtual method of the basic logic of opening a deposit.
        /// </summary>
        /// <param name="depositClient">client</param>
        /// <param name="amount">deposit amount of money</param>
        /// <param name="msg">open deposit message</param>
        /// <returns></returns>
        public virtual decimal OpenDeposit(Client depositClient, decimal amount, StringBuilder message)
        {
            return Balance;
        }

        /// <summary>
        /// virtual method of the basic logic for requesting a loan
        /// </summary>
        /// <param name="loanClient">client</param>
        /// <param name="amount">loan amount</param>
        /// <param name="message">message after loan operation</param>
        /// <returns></returns>
        public virtual decimal RequestLoan(Client loanClient, decimal amount, StringBuilder message)
        {
            return Balance;
        }

        ///// <summary>
        ///// virtual calculation of Interest Rate
        ///// </summary>
        ///// <returns></returns>
        //protected virtual decimal CalcInterestRate()
        //{
        //    return 0;
        //}

        /// <summary>
        /// virtual generation of deposit or loan number
        /// </summary>
        /// <returns></returns>
        protected virtual string GenerateDepLoanNumber()
        {
            return string.Empty;
        }

    }
}