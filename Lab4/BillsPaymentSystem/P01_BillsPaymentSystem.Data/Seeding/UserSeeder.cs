using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.Seeding
{
    public class UserSeeder : ISeeder<User>
    {
        public void Seed(EntityTypeBuilder<User> builder)
        {
            List<User> users = new List<User>()
            {
                new User
                {
                    UserId = 1,
                    Email = "first@gmail.com",
                    FirstName = "Tomas",
                    LastName = "Shelby",
                    Password = "12345"
                }
            };
            builder.HasData(users);
        }
    }
}
