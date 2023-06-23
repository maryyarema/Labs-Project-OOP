﻿using System.ComponentModel.DataAnnotations.Schema;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public decimal Limit { get; set; }
        public decimal MoneyOwed { get; set; }

        [NotMapped]
        public decimal LimitLeft => Limit - MoneyOwed;
        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (LimitLeft >= amount)
            {
                MoneyOwed += amount;
            }
            else
            {
                Console.WriteLine("Insufficient funds!");
            }
        }

        public void Deposit(decimal amount)
        {
            MoneyOwed -= amount;
        }
    }
}
