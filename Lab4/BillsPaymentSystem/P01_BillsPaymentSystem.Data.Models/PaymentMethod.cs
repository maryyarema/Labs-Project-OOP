namespace P01_BillsPaymentSystem.Data.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public PaymentMethodType.Type Type { get; set; }
        public int UserId { get; set; }
        public int? BankAccountId { get; set; }
        public int? CreditCardId { get; set; }

        public CreditCard CreditCard { get; set; }
        public BankAccount BankAccount { get; set; }
        public User User { get; set; }

    }
}
