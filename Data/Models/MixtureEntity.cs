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

        public virtual ICollection<PropertyMixtureEntity>? PropertyMixtureEntities { get; init; }
        public virtual ICollection<RecipeEntity>? RecipeEntities { get; init; }

        public virtual ICollection<CompositionRecipeEntity>? CompositionRecipeEntities { get; init; }
    }
}
