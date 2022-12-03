using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("property_usefulProduct")]
public record PropertyUsefulProductEntity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Required]
    public int Id { get; init; }

    [Column("id_property")]
    [Required]
    public int IdProperty { get; init; }
    [ForeignKey("IdProperty")]
    public virtual PropertyEntity Property { get; init; }


    [Column("id_usefulProduct")]
    [Required]
    public int IdUsefulProduct { get; init; }
    [ForeignKey("IdUsefulProduct")]
    public UsefulProductEntity UsefulProduct { get; init; }

    [Column("value")]
    [Required]
    public double Value { get; init; }
}