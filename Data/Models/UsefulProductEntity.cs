using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polimer.Data.Models
{
    [Table("useful_product")]
    public record UsefulProductEntity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Required]
        public int Id { get; init; }

        [Column("name")]
        [Required]
        public string? Name { get; init; }

        [Column("id_recipe")]
        [Required]
        public int IdRecipe { get; init; }
        [ForeignKey("IdRecipe")]
        public RecipeEntity Recipe { get; init; }
        
        public virtual ICollection<PropertyUsefulProductEntity>? UsefulProductProperties { get; init; }
    }
}
