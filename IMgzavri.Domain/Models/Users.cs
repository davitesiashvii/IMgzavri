

namespace IMgzavri.Domain.Models
{
    public class Users
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string MobileNumber { get; set; }

        public string IdNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public int? RendomCode { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }

}
