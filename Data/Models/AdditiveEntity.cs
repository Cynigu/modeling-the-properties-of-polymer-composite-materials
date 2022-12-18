using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("additive")]
public record AdditiveEntity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Required]
    public int Id { get; init; }

    [Column("name")]
    [Required]
    public string? RussianName { get; init; }

    [Column("short_name")]
    [Required]
    public string? QuickName { get; init; }

    [Column("english_name")]
    [Required]
    public string? EnglishName { get; init; }

    public virtual ICollection<RecipeEntity>? RecipeEntities { get; init; }

    public virtual ICollection<PropertyAdditiveEntity>? AdditiveProperies { get; init; }
}