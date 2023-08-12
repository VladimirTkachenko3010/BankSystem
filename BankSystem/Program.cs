using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Application;

var bank = new Bank<Client>();
var message = new StringBuilder();
var name = string.Empty;
var accountNumber = string.Empty;
var clientApp = new ClientApp();


Console.WriteLine("1 - Display clients information");
Console.WriteLine("2 - Add regular client");
Console.WriteLine("3 - Add VIP client");
Console.WriteLine("4 - Add legal entity");
Console.WriteLine("5 - Delete client by account number");
Console.WriteLine("6 - Perform funds transfer");
Console.WriteLine("7 - Opening deposits");
Console.WriteLine("8 - Opening credits");

#region creating test clients
var client1 = new RegularClient("REGULAR_John", bank.GenerateUniqueAccountNumber());
var client2 = new VIPClient("VIP_Alice", bank.GenerateUniqueAccountNumber());
var regularClient = new RegularClient("REGULAR_Иван", bank.GenerateUniqueAccountNumber());
var vipClient = new VIPClient("VIP_Анна", bank.GenerateUniqueAccountNumber());
var legalEntity = new LegalEntity("ООО \"Finance\"", bank.GenerateUniqueAccountNumber());
bank.AddClient(client1);
bank.AddClient(client2);
bank.AddClient(regularClient);
bank.AddClient(vipClient);
bank.AddClient(legalEntity);
#endregion

while (true)
{
    string input = Console.ReadLine()!;

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

                try
                {
                    Console.Write("Enter client name: ");
                    name = Console.ReadLine();

                    while (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter a valid Regular Client name:");
                        name = Console.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred while reading input. >>{e.Message}  {e.GetType}");
                }
                bank.AddClient(RegularClient.CreateRegularClient(name!, bank.GenerateUniqueAccountNumber()));
                break;

            case 3:

                try
                {
                    Console.Write("Enter VIP client name: ");
                    name = Console.ReadLine();
                    while (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter a valid VIP Client name:");
                        name = Console.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred while reading input. >>{e.Message}  {e.GetType}");
                }
                bank.AddClient(VIPClient.CreateVIPClient(name!, bank.GenerateUniqueAccountNumber()));
                break;

            case 4:

                try
                {
                    Console.Write("Enter Legal Entity name: ");
                    name = Console.ReadLine();

                    while (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter a valid Legal Entity name:");
                        name = Console.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred while reading input. >>{e.Message}  {e.GetType}");
                }
                bank.AddClient(LegalEntity.CreateLegalEntity(name!, bank.GenerateUniqueAccountNumber()));
                break;

            case 5:

                try
                {
                    Console.WriteLine("Enter the account number to delete:");
                    accountNumber = Console.ReadLine();
                    while ((string.IsNullOrEmpty(accountNumber)) && (bank.FindClientByAccountNumber(accountNumber!) == null))
                    {
                        Console.WriteLine("Incorrect account number entry. Please enter right accout number:");
                        accountNumber = Console.ReadLine()!;
                    }
                    bank.RemoveClient(bank.FindClientByAccountNumber(accountNumber!));
                    Console.WriteLine("Account deleted");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error while finding by account number. >>{e.Message}  {e.GetType}");
                }
                break;

            case 6:


                try
                {
                    Console.WriteLine("Enter the account number of the sender of funds:");
                    accountNumber = Console.ReadLine();
                    while ((string.IsNullOrEmpty(accountNumber)) && (bank.FindClientByAccountNumber(accountNumber!) == null))
                    {
                        Console.WriteLine("Incorrect account number entry. Please enter right accout number:");
                        accountNumber = Console.ReadLine()!;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error while finding by account number. >>{e.Message}  {e.GetType}");
                }
                var sender = bank.FindClientByAccountNumber(accountNumber!);

                try
                {
                    Console.WriteLine("Enter the account number of the recipient of funds:");
                    accountNumber = Console.ReadLine();
                    while ((string.IsNullOrEmpty(accountNumber)) && (bank.FindClientByAccountNumber(accountNumber!) == null))
                    {
                        Console.WriteLine("Incorrect account number entry. Please enter right accout number:");
                        accountNumber = Console.ReadLine()!;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error while finding by account number. >>{e.Message}  {e.GetType}");
                }
                var recipient = bank.FindClientByAccountNumber(accountNumber!);


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
                    message.Clear();
                    clientApp.Transfer(sender, recipient, transferAmount, message);
                    Console.WriteLine($"MESSAGE : {message}");
                }
                else
                {
                    Console.WriteLine("The sender with the specified account number was not found.");
                }
                break;

            case 7:

                try
                {
                    Console.WriteLine("Enter the account number to open a deposit:");
                    accountNumber = Console.ReadLine();
                    while ((string.IsNullOrEmpty(accountNumber)) && (bank.FindClientByAccountNumber(accountNumber!) == null))
                    {
                        Console.WriteLine("Incorrect account number entry. Please enter right accout number:");
                        accountNumber = Console.ReadLine()!;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error while finding by account number. >>{e.Message}  {e.GetType}");
                }

                var depositClient = bank.FindClientByAccountNumber(accountNumber!);
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
                    message.Clear();
                    clientApp.OpenDeposit(depositClient, depositAmount, message);
                    Console.WriteLine($"MESSAGE : \n{message}");
                }
                else
                {
                    Console.WriteLine("Incorrect account number");
                }
                break;

            case 8:

                try
                {
                    Console.WriteLine("Enter the account number for opening a loan (replenishment of the balance):");
                    accountNumber = Console.ReadLine();
                    while ((string.IsNullOrEmpty(accountNumber)) && (bank.FindClientByAccountNumber(accountNumber!) == null))
                    {
                        Console.WriteLine("Incorrect account number entry. Please enter right accout number:");
                        accountNumber = Console.ReadLine()!;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error while finding by account number. >>{e.Message}  {e.GetType}");
                }
                var loanClient = bank.FindClientByAccountNumber(accountNumber!);

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
                    message.Clear();
                    clientApp.RequestLoan(loanClient, loanAmount, message);
                    Console.WriteLine($"MESSAGE : \n{message}");
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