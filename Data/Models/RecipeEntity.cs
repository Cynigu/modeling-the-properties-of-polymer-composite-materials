using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("recipe")]
public record RecipeEntity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Required]
    public int Id { get; init; }

    [Column("id_compatibility")]
    [Required]
    public int IdCompatibilityMaterial { get; init; }
    [ForeignKey("IdCompatibilityMaterial")]
    public virtual CompatibilityMaterialEntity CompatibilityMaterial { get; init; }

    [Column("id_additive")]
    [Required]
    public int IdAdditive { get; init; }
    [ForeignKey("IdAdditive")]
    public virtual AdditiveEntity Additive { get; init; }

    [Column("content_material1")]
    public double ContentMaterialFirst { get; init; }
    
    [Column("content_material2")]
    public double ContentMaterialSecond { get; init; }

    [Column("content_additive")]
    public double ContentAdditive { get; init; }

    public  ICollection<UsefulProductEntity> UsefulProducts { get; init; }
}