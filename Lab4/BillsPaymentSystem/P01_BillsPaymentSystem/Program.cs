using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

public class Program
{
    private static BillsPaymentSystemContext _context = new BillsPaymentSystemContext();
    private static void Main()
    {
        int userId = 0;

        Console.WriteLine("Enter an userId: ");
        userId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter amount to pay: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        using (var context = _context)
        {
            var user = context.Users
                .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.CreditCard)
                .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.BankAccount)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                Console.WriteLine($"User with id {userId} not found.");
                return;
            }

            PayBills(userId, amount);

            Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            Console.WriteLine("Bank Accounts:");

            foreach (var bankAccount in user.PaymentMethods.Where(pm => pm.BankAccount != null).Select(pm => pm.BankAccount))
            {
                Console.WriteLine($"-- ID: {bankAccount.BankAccountId}");
                Console.WriteLine($"--- Balance: {bankAccount.Balance:F2}");
                Console.WriteLine($"--- Bank: {bankAccount.BankName}");
                Console.WriteLine($"--- SWIFT: {bankAccount.SWIFTCode}");
            }

            Console.WriteLine("Credit Cards:");

            foreach (var creditCard in user.PaymentMethods.Where(pm => pm.CreditCard != null).Select(pm => pm.CreditCard))
            {
                Console.WriteLine($"-- ID: {creditCard.CreditCardId}");
                Console.WriteLine($"--- Limit: {creditCard.Limit:F2}");
                Console.WriteLine($"--- Money Owed: {creditCard.MoneyOwed:F2}");
                Console.WriteLine($"--- Limit Left: {(creditCard.Limit - creditCard.MoneyOwed):F2}");
                Console.WriteLine($"--- Expiration Date: {creditCard.ExpirationDate:yyyy/MM}");
            }

            
        }

    }
    public static void PayBills(int userId, decimal amount)
    {
        using (var db = _context)
        {
            var user = db.Users.Include(u => u.PaymentMethods)
                               .ThenInclude(pm => pm.BankAccount)
                               .Include(u => u.PaymentMethods)
                               .ThenInclude(pm => pm.CreditCard)
                               .SingleOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                Console.WriteLine($"User with ID {userId} does not exist.");
                return;
            }

            var bankAccounts = user.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.Type.BankAccount)
                                                  .OrderBy(pm => pm.BankAccountId)
                                                  .Select(pm => pm.BankAccount);

            var creditCards = user.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.Type.CreditCard)
                                                 .OrderBy(pm => pm.CreditCardId)
                                                 .Select(pm => pm.CreditCard);

            foreach (var bankAccount in bankAccounts)
            {
                if (amount <= 0)
                {
                    break;
                }

                var withdrawAmount = Math.Min(amount, bankAccount.Balance);
                bankAccount.Withdraw(withdrawAmount);
                amount -= withdrawAmount;
            }

            foreach (var creditCard in creditCards)
            {
                if (amount <= 0)
                {
                    break;
                }

                var withdrawAmount = Math.Min(amount, creditCard.LimitLeft);
                creditCard.Withdraw(withdrawAmount);
                amount -= withdrawAmount;
            }

            if (amount > 0)
            {
                Console.WriteLine("Insufficient funds!");
            }

            db.SaveChanges();
        }
    }
}