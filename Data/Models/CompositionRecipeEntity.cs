using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("composition_recipe")]
public record CompositionRecipeEntity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Required]
    public int Id { get; init; }

    [Column("id_mixture")]
    [Required]
    public int IdMixture { get; init; }
    [ForeignKey("IdMixture")]
    public MixtureEntity Mixture { get; init; }

    [Column("id_recipe")]
    [Required]
    public int IdRecipe { get; init; }
    [ForeignKey("IdRecipe")]
    public RecipeEntity Recipe { get; init; }
}