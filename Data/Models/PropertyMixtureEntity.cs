using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("property_mixture")]
public record PropertyMixtureEntity : IEntity
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
    public PropertyEntity Property { get; init; }


    [Column("id_mixture")]
    [Required]
    public int IdMixture { get; init; }
    [ForeignKey("IdMixture")]
    public MixtureEntity Mixture { get; init; }
}