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
        /// <param name="message">transfer message</param>
        /// <returns></returns>
        public virtual decimal Transfer(Client sender, Client reciepent, decimal amount, StringBuilder message)
        {
            if (reciepent != null)
            {
                if (sender.Balance >= amount)
                {
                    sender.Balance -= amount;
                    reciepent.Balance += amount;
                    message.Append($"The transfer of {amount} hryvnias was completed successfully.");
                }
                else
                {
                    message.Append("Unfortunately, there are insufficient funds in the account.");
                }
                return reciepent.Balance;
            }
            else
            {
                message.Append("The recipient with the specified account number was not found.");
                return sender.Balance;
            }
        }


        /// <summary>
        /// base method of the basic logic of opening a deposit.
        /// </summary>
        /// <param name="depositClient">client</param>
        /// <param name="amount">deposit amount of money</param>
        /// <param name="message">open deposit message</param>
        /// <returns></returns>
        public decimal OpenDeposit(Client depositClient, decimal amount, StringBuilder message)
        {
            if (amount < depositClient.MinDepAmount)
            {
                message.Append($"Insufficient deposit opening amount. Minimum amount = {depositClient.MinDepAmount}");
                return 0;
            }

            //generation of a unique deposit number
            string depNumber = GenerateDepLoanNumber();
            decimal interestRate = CalcInterestRate(depositClient);

            //Balance update
            depositClient.Balance += amount;

            message.Append($"A deposit of {amount} hryvnias has been successfully opened.\nInterest rate: {interestRate}.\nDeposit number: {depNumber}.");
            return depositClient.Balance;
        }


        /// <summary>
        /// base method of the basic logic for requesting a loan
        /// </summary>
        /// <param name="loanClient">client</param>
        /// <param name="amount">loan amount</param>
        /// <param name="message">message after loan operation</param>
        /// <returns></returns>
        public decimal RequestLoan(Client loanClient, decimal amount, StringBuilder message)
        {

            if (amount < loanClient.MinCreditAmount)
            {
                message.Append($"Insufficient amount for issuing a loan. Minimum amount = {loanClient.MinCreditAmount}");
                return loanClient.Balance;
            }
            decimal interestRate = CalcInterestRate(loanClient);
            string loanNumber = GenerateDepLoanNumber();

            //Balance update
            loanClient.Balance += amount;

            message.Append($"Credit for the amount of {amount} hryvnias has been successfully issued.\nInterest rate: {interestRate}%.\nLoan number: {loanNumber}");
            return loanClient.Balance;
        }


        /// <summary>
        /// Base calculation of Interest Rate
        /// </summary>
        /// <returns></returns>
        protected decimal CalcInterestRate(Client client)
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