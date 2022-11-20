using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("units")]
public record UnitEntity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Required]
    public int Id { get; init; }

    [Column("name")]
    [Required]
    public string? Name { get; init; }

    public virtual ICollection<PropertyEntity>? Properties { get; init; }
}