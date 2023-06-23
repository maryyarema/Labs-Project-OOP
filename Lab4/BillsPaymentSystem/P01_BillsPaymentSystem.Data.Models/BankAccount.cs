using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public decimal Balance { get; set; }

        [MaxLength(50)]
        public string BankName { get; set; }

        [MaxLength(20)]
        public string SWIFTCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Insufficient funds!");
            }
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
    }
}
