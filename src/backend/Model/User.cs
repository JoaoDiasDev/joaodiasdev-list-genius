using JoaoDiasDev.ListGenius.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoaoDiasDev.ListGenius.Model
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;

        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("enabled")]
        public bool Enabled { get; set; } = true;

        [Column("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [Column("refresh_token_expiry_time")]
        public DateTime RefreshTokenUntilExpirationTime { get; set; }
    }
}
