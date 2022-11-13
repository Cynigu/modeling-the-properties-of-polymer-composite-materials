using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models
{
    [Table("users")]
    public record UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Required]
        public int Id { get; init; }

        [Column("login")]
        [Required]
        public string? Login { get; init; }

        [Column("password")]
        [Required]
        public string? Password { get; init; }

        [Column("role")]
        [Required]
        public string? Role { get; init; }
    }
}
