using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.Seeding
{
    public class CreditCardSeeder : ISeeder<CreditCard>
    {
        public void Seed(EntityTypeBuilder<CreditCard> builder)
        {
            List<CreditCard> creditCards = new List<CreditCard>()
            {
                new CreditCard
                {
                    CreditCardId = 1,
                    Limit = 2500.00M,
                    MoneyOwed = 500.00M,
                    ExpirationDate = new DateTime(2022, 3, 4)
                }
            };
            builder.HasData(creditCards);
        }
    }
}
