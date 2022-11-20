using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("property_material")]
public record PropertyMaterialEntity : IEntity
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


    [Column("id_material")]
    [Required]
    public int IdMaterial { get; init; }
    [ForeignKey("IdMaterial")]
    public virtual MaterialEntity Material { get; init; }

}