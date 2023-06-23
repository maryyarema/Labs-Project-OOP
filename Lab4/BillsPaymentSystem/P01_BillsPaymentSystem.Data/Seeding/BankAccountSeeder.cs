using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.Seeding
{
    public class BankAccountSeeder : ISeeder<BankAccount>
    {
        public void Seed(EntityTypeBuilder<BankAccount> builder)
        {
            List<BankAccount> bankAccounts = new List<BankAccount>()
            {
                new BankAccount
                {
                    BankAccountId = 1,
                    Balance = 1000.00M,
                    BankName = "PrivateBank",
                    SWIFTCode = "1000 0000 0000 0000"
                }
            };
            builder.HasData(bankAccounts);
        }
    }
}
