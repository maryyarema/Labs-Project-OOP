using System.ComponentModel.DataAnnotations.Schema;

namespace P01_BillsPaymentSystem.Data.Models
{
    [NotMapped]
    public class PaymentMethodType
    {
        public enum Type
        {
            BankAccount,
            CreditCard
        }

    }
}
