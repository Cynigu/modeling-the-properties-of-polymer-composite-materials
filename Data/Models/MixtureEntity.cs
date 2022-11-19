using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Polimer.Data.Models
{
    [Table("mixture")]
    public record MixtureEntity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Required]
        public int Id { get; init; }

        [Column("name")]
        [Required]
        public string? Name { get; init; }

        public ICollection<PropertyMixtureEntity>? PropertyMixtureEntities { get; init; }
        public ICollection<RecipeEntity>? RecipeEntities { get; init; }

        public ICollection<CompositionRecipeEntity>? CompositionRecipeEntities { get; init; }
    }
}
