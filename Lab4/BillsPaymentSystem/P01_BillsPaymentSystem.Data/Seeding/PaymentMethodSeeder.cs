using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.Seeding
{
    public class PaymentMethodSeeder : ISeeder<PaymentMethod>
    {
        public void Seed(EntityTypeBuilder<PaymentMethod> builder)
        {
            List<PaymentMethod> paymentMethod = new List<PaymentMethod>()
            {
                new PaymentMethod
                {
                    PaymentMethodId = 1,
                    CreditCardId = 1,
                    Type = PaymentMethodType.Type.CreditCard,
                    UserId = 1
                }
            };
            builder.HasData(paymentMethod);
        }
    }
}
