using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    /// <summary>
    /// Bank class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Bank<T> where T : Client
    {
        private readonly List<T> clients;
        private static readonly HashSet<string> usedAccountNumbers = new();

        public Bank()
        {
            clients = new List<T>();
        }

        /// <summary>
        /// finding client by acc number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public T FindClientByAccountNumber(string accountNumber)
        {
            return clients.FirstOrDefault(c => c.AccountNumber == accountNumber)!;
        }

        /// <summary>
        /// generation unique acc number method
        /// </summary>
        /// <returns></returns>
        public static string GenerateUniqueAccountNumber()
        {
            const int accountNumberLength = 5;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // symbols for acc number
            Random random = new();
            string accountNumber;

            do
            {
                accountNumber = new string(Enumerable.Repeat(chars, accountNumberLength)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (usedAccountNumbers.Contains(accountNumber));

            usedAccountNumbers.Add(accountNumber);
            return accountNumber;
        }

        public void AddClient(T client)
        {
            clients.Add(client);
        }

        public void RemoveClient(T client)
        {
            clients.Remove(client);
        }

        public void Transfer(T sender, T recipient, decimal amount, string msg)
        {
            sender.Transfer(recipient, amount, msg);
        }

        public List<T> Clients
        {
            get { return clients; }
        }
    }
}