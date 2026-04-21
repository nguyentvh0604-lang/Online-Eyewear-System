using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("category_id")]
        [Required]
        public int CategoryId { get; set; }

        [Column("name")]
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Column("product_type")]
        [Required]
        public string ProductType { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("image_2d_url")]
        [MaxLength(500)]
        public string? Image2dUrl { get; set; }

        [Column("image_3d_url")]
        [MaxLength(500)]
        public string? Image3dUrl { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}