namespace SchoolLibrary_EF.DAL.Entities.Identity
{
    public class JWT
    {
        // The Super Secret Key that will be used for Encryption
        public string? Key { get; set; }
        // Identifies the principle that issued the JWT
        public string? Issuer { get; set; }
        // Identifies the recipients that the JWT is intended for
        public string? Audience { get; set; }
        // Defines the Minutes the generated JWT will remain valid
        public double DurationInMinutes { get; set; }
    }
}