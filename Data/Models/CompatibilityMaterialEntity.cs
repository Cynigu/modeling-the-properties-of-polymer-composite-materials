 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("compatibility")]
public record CompatibilityMaterialEntity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Required]
    public int Id { get; init; }

    [Column("id_material1")]
    [Required]
    public int IdFirstMaterial { get; init; }
    [ForeignKey("IdFirstMaterial")]
    public virtual MaterialEntity FirstMaterial { get; init; }

    [Column("id_material2")]
    [Required]
    public int IdSecondMaterial { get; init; }
    [ForeignKey("IdSecondMaterial")]
    public virtual MaterialEntity SecondMaterial { get; init; }

    public virtual ICollection<RecipeEntity>? Recipes { get; init; }
}