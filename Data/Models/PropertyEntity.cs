using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.Data.Models;

[Table("property")]
public record PropertyEntity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Required]
    public int Id { get; init; }

    [Column("name")]
    [Required]
    public string? Name { get; init; }

    [Column("id_unit")]
    public int UnitId { get; set; }
    [ForeignKey("UnitId")]
    public virtual UnitEntity Unit { get; init; }

    [InverseProperty("Property")]
    public  virtual ICollection<PropertyMaterialEntity>? PropertyMaterials { get; init; }
    public virtual ICollection<PropertyMixtureEntity>? PropertyMixtureEntities { get; init; }
}