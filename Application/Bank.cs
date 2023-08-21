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
        private readonly List<string> usedAccountNumbers = new();
        const int accountNumberLength = 5;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // symbols for acc number

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
            ////need to move the exception to the menu
            //try
            //{
            //    while (string.IsNullOrEmpty(accountNumber))
            //    {
            //        Console.WriteLine("Account number cannot be empty. Please enter right accout number:");
            //        accountNumber = Console.ReadLine()!;
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"An error while finding by account number. >>{e.Message}  {e.GetType}");
            //}
            return clients.FirstOrDefault(c => c.AccountNumber == accountNumber)!;
        }

        /// <summary>
        /// generation unique acc number method
        /// </summary>
        /// <returns></returns>
        public string GenerateUniqueAccountNumber()
        {
            var random = new Random();
            var accountNumber = string.Empty;

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

        public bool RemoveClient(T client)
        {
            if (clients.Contains(client))
            {
                clients.Remove(client);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Transfer(T sender, T recipient, decimal amount, StringBuilder message)
        {
            Transfer(sender, recipient, amount, message);
        }

        public List<T> Clients
        {
            get { return clients; }
        }
    }
}