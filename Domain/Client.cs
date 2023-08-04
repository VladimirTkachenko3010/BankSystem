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
        /// <param name="msg">message after method</param>
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
    }

}