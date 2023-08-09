using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ClientApp
    {
        /// <summary>
        /// transfer money to another account
        /// </summary>*
        /// <param name="sender">sender of money</param>
        /// <param name="reciepent">receiepent of money</param>
        /// <param name="amount">transfer money amount</param>
        /// <param name="msg">transfer message</param>
        /// <returns></returns>
        public virtual (decimal Balance, string msg) Transfer(Client sender, Client reciepent, decimal amount, string msg)
        {
            if (reciepent != null)
            {
                if (sender.Balance >= amount)
                {
                    sender.Balance -= amount;
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
                return (sender.Balance, msg);
            }
        }


        /// <summary>
        /// base method of the basic logic of opening a deposit.
        /// </summary>
        /// <param name="depositClient">client</param>
        /// <param name="amount">deposit amount of money</param>
        /// <param name="msg">open deposit message</param>
        /// <returns></returns>
        public (decimal Balance, string msg) OpenDeposit(Client depositClient, decimal amount, string msg)
        {
            if (amount < depositClient.MinDepAmount)
            {
                msg = $"Insufficient deposit opening amount. Minimum amount = {depositClient.MinDepAmount}";
                return (0, msg);
            }

            //generation of a unique deposit number
            string depNumber = GenerateDepLoanNumber();
            decimal interestRate = CalcInterestRate();

            //Balance update
            depositClient.Balance += amount;

            msg = $"A deposit of {amount} hryvnias has been successfully opened.\n"
                + $"Interest rate: {interestRate}.\n"
                + $"Deposit number: {depNumber}.";
            return (depositClient.Balance, msg);
        }


        /// <summary>
        /// base method of the basic logic for requesting a loan
        /// </summary>
        /// <param name="loanClient">client</param>
        /// <param name="amount">loan amount</param>
        /// <param name="msg">message after loan operation</param>
        /// <returns></returns>
        public (decimal Balance, string msg) RequestLoan(Client loanClient, decimal amount, string msg)
        {

            if (amount < loanClient.MinCreditAmount)
            {
                msg = $"Insufficient amount for issuing a loan. Minimum amount = {loanClient.MinCreditAmount}";
                return (loanClient.Balance, msg);
            }
            decimal interestRate = CalcInterestRate();
            string loanNumber = GenerateDepLoanNumber();

            //Balance update
            loanClient.Balance += amount;

            msg = $"Credit for the amount of {amount} hryvnias has been successfully issued.\n"
                + $"Interest rate: {interestRate}%.\n"
                + $"Loan number: {loanNumber}";
            return (loanClient.Balance, msg);
        }


        /// <summary>
        /// Base calculation of Interest Rate
        /// </summary>
        /// <returns></returns>
        protected decimal CalcInterestRate()
        {
            return 0;
        }

        /// <summary>
        /// base generation of deposit or loan number
        /// </summary>
        /// <returns></returns>
        protected string GenerateDepLoanNumber()
        {
            // генерация ун кода номера вклада
            return Guid.NewGuid().ToString();
        }

    }
}