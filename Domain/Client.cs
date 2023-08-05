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
        protected abstract decimal MinDepAmount { get; }
        /// <summary>
        /// minimum credit amount for client
        /// </summary>
        protected abstract decimal MinCreditAmount { get; }

        public Client(string name, string accountNumber)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = 0;
        }

        /// <summary>
        /// transfer money to another account
        /// </summary>
        /// <param name="reciepent">receiepent of money</param>
        /// <param name="amount">transfer money amount</param>
        /// <param name="msg">transfer message</param>
        /// <returns></returns>
        public virtual (decimal Balance, string msg) Transfer(Client reciepent, decimal amount, string msg)
        {
            if (reciepent != null)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                    reciepent.Balance += amount;
                    msg = $"The transfer of {amount} hryvnias was completed successfully.";
                }
                else
                {
                    msg = "Unfortunately, there are insufficient funds in the account.";
                }
                return (reciepent.Balance, msg);
            }
            else
            {
                msg = "The recipient with the specified account number was not found.";
                return (Balance, msg);
            }
        }

        /// <summary>
        /// Virtual method of the basic logic of opening a deposit.
        /// </summary>
        /// <param name="amount">deposit amount of money</param>
        /// <param name="msg">open deposit message</param>
        /// <returns></returns>
        public virtual (decimal Balance, string msg) OpenDeposit(decimal amount, string msg)
        {
            if (amount < MinDepAmount)
            {
                msg = $"Insufficient deposit opening amount. Minimum amount = {MinDepAmount}";
                return (0, msg);
            }

            //generation of a unique deposit number
            string depNumber = GenerateDepLoanNumber();
            decimal interestRate = CalcInterestRate();

            //Balance update
            Balance += amount;

            msg = $"A deposit of {amount} hryvnias has been successfully opened.\n"
                + $"Interest rate: {interestRate}.\n"
                + $"Deposit number: {depNumber}.";
            return (Balance, msg);
        }

        /// <summary>
        /// Virtual method of the basic logic for requesting a loan
        /// </summary>
        /// <param name="amount">loan amount</param>
        /// <param name="msg">message after loan operation</param>
        /// <returns></returns>
        public virtual (decimal Balance, string msg) RequestLoan(decimal amount, string msg)
        {

            if (amount < MinCreditAmount)
            {
                msg = $"Insufficient amount for issuing a loan. Minimum amount = {MinCreditAmount}";
                return (Balance, msg);
            }
            decimal interestRate = CalcInterestRate();
            string loanNumber = GenerateDepLoanNumber();

            //Balance update
            Balance += amount;

            msg = $"Credit for the amount of {amount} hryvnias has been successfully issued.\n"
                + $"Interest rate: {interestRate}%.\n"
                + $"Loan number: {loanNumber}";
            return (Balance, msg);
        }

        /// <summary>
        /// Base calculation of Interest Rate
        /// </summary>
        /// <returns></returns>
        protected virtual decimal CalcInterestRate()
        {
            return 0;
        }

        /// <summary>
        /// Generation of deposit or loan number
        /// </summary>
        /// <returns></returns>
        protected virtual string GenerateDepLoanNumber()
        {
            // генерация ун кода номера вклада
            return Guid.NewGuid().ToString();
        }
    }

}