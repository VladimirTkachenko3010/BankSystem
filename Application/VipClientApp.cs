using Application.Services.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class VipClientApp
    {
        private const decimal VipClientMinDep = 5000; // Minimum deposit amount for a VIP client
        public decimal MinDepAmount => VipClientMinDep;
        public decimal MinCreditAmount => 10000;

        private readonly IClientService _clientService;

        public VipClientApp(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// deposit method for VIP clients
        /// </summary>
        /// <param name="amount"></param>
        public ClientResponse OpenDeposit(Client depositClient, decimal amount, StringBuilder message) 
        {

            var modifiedAmount = _clientService.OpenDeposit(depositClient, amount, message);
            depositClient.Balance += modifiedAmount;
            message.Append($"\nVIP client: opening a deposit in the amount of {amount}.\n");
            return new ClientResponse(depositClient.Balance, message);
        }
    }
}
