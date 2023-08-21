using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IClientService
    {
        /// <summary>
        /// Iclient service open deposit
        /// </summary>
        /// <param name="depositClient"></param>
        /// <param name="amount"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public decimal OpenDeposit(Client depositClient, decimal amount, StringBuilder message);
        public decimal CalcInterestRate(Client client);
    }
}
