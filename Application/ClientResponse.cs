using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ClientResponse
    {
        public decimal Balance { get; set; }
        public StringBuilder Message { get; set; } = null!;

        public ClientResponse(decimal balance, StringBuilder message)
        {
            this.Balance = balance;
            this.Message = message;
        }
    }
}
