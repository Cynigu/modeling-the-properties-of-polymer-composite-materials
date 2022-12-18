using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("property_additive")]
public record PropertyAdditiveEntity : IEntity
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


    [Column("id_additive")]
    [Required]
    public int IdAdditive { get; init; }
    [ForeignKey("IdAdditive")]
    public AdditiveEntity Additive { get; init; }

    [Column("value")]
    [Required]
    public double Value { get; init; }
}