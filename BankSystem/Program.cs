using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Application;

var bank = new Bank<Client>();
var msg = string.Empty;

Console.WriteLine("1 - Display clients information");
Console.WriteLine("2 - Add regular client");
Console.WriteLine("3 - Add VIP client");
Console.WriteLine("4 - Add legal entity");
Console.WriteLine("5 - Delete client by account number");
Console.WriteLine("6 - Perform funds transfer");
Console.WriteLine("7 - Opening deposits");
Console.WriteLine("8 - Opening credits");

#region creating test clients
var client1 = new RegularClient("REGULAR_John", Bank<Client>.GenerateUniqueAccountNumber());
var client2 = new VIPClient("VIP_Alice", Bank<Client>.GenerateUniqueAccountNumber());
var regularClient = new RegularClient("REGULAR_Иван", Bank<Client>.GenerateUniqueAccountNumber());
var vipClient = new VIPClient("VIP_Анна", Bank<Client>.GenerateUniqueAccountNumber());
var legalEntity = new LegalEntity("ООО \"Finance\"", Bank<Client>.GenerateUniqueAccountNumber());
bank.AddClient(client1);
bank.AddClient(client2);
bank.AddClient(regularClient);
bank.AddClient(vipClient);
bank.AddClient(legalEntity);
#endregion

while (true)
{
    string input = Console.ReadLine();

    if (int.TryParse(input, out int key))
    {
        switch (key)
        {
            case 1:
                foreach (var client in bank.Clients)
                {
                    Console.WriteLine($"Name: {client.Name}");
                    Console.WriteLine($"Account number: {client.AccountNumber}");
                    Console.WriteLine($"Balance: {client.Balance}");
                    Console.WriteLine();
                }
                break;

            case 2:
                bank.AddClient(RegularClient.CreateRegularClient(Bank<Client>.GenerateUniqueAccountNumber()));
                break;

            case 3:
                bank.AddClient(VIPClient.CreateVIPClient(Bank<Client>.GenerateUniqueAccountNumber()));
                break;

            case 4:
                bank.AddClient(LegalEntity.CreateLegalEntity(Bank<Client>.GenerateUniqueAccountNumber()));
                break;

            case 5:
                Console.WriteLine("Enter the account number to delete:");
                var removeClient = bank.FindClientByAccountNumber(Console.ReadLine());
                if (removeClient == null)
                {
                    Console.WriteLine("Account not deleted, incorrect account number entry");
                }
                else
                {
                    bank.RemoveClient(removeClient);
                    Console.WriteLine("Account deleted");
                }
                break;

            case 6:

                Console.WriteLine("Enter the account number of the sender of funds:");
                var sender = bank.FindClientByAccountNumber(Console.ReadLine());
                Console.WriteLine("Enter the account number of the recipient of funds:");
                var recipient = bank.FindClientByAccountNumber(Console.ReadLine());
                decimal transferAmount;
                do
                {
                    Console.Write("Enter transfer amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out transferAmount))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect value of the transfer amount. Please enter a valid number.");
                    }
                } while (true);

                if (sender != null)
                {
                    //need to fix console wrtiline message
                    
                    Console.WriteLine($"MESSAGE : \n{sender.Transfer(recipient, transferAmount, msg).msg}");
                }
                else
                {
                    Console.WriteLine("The sender with the specified account number was not found.");
                }
                break;

            case 7:
                Console.WriteLine("Enter the account number to open a deposit:");
                var depositClient = bank.FindClientByAccountNumber(Console.ReadLine());
                if (depositClient != null)
                {
                    decimal depositAmount;
                    do
                    {
                        Console.Write("Enter deposit amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out depositAmount))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect value of the deposit amount. Please enter a valid number.");
                        }
                    } while (true);

                    Console.WriteLine($"MESSAGE : \n{depositClient.OpenDeposit(depositAmount, msg).msg}");
                }
                else
                {
                    Console.WriteLine("Incorrect account number");
                }
                break;

            case 8:
                Console.WriteLine("Enter the account number for opening a loan (replenishment of the balance):");
                var loanClient = bank.FindClientByAccountNumber(Console.ReadLine());
                if (loanClient != null)
                {
                    decimal loanAmount;
                    do
                    {
                        Console.Write("Enter loan amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out loanAmount))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect value of the loan amount. Please enter a valid number.");
                        }
                    } while (true);

                    //need to fix console wrtiline message
                    Console.WriteLine($"MESSAGE = {loanClient.RequestLoan(loanAmount, msg).msg}");
                }
                else
                {
                    Console.WriteLine("Incorrect account number");
                }
                break;

            default:
                Console.WriteLine("Invalid choice. Please enter a valid option.");
                break;
        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("Incorrect input menu-choise, try again:");
    }
}