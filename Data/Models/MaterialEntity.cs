using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("material")]
public record MaterialEntity : IEntity
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

    public virtual ICollection<PropertyMaterialEntity>? MaterialProperties { get; init; }

    [InverseProperty("FirstMaterial")]
    public virtual ICollection<CompatibilityMaterialEntity>? MaterialsFirst{ get; init; }

    [InverseProperty("SecondMaterial")]
    public virtual ICollection<CompatibilityMaterialEntity>? MaterialsSecond { get; init; }
}