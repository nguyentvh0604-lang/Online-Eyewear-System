using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("product_variants")]
    public class ProductVariant
    {
        [Key]
        [Column("variant_id")]
        public int VariantId { get; set; }

        [Column("product_id")]
        [Required]
        public int ProductId { get; set; }

        [Column("color")]
        [MaxLength(50)]
        public string? Color { get; set; }

        [Column("size")]
        [MaxLength(50)]
        public string? Size { get; set; }

        [Column("frame_material")]
        [MaxLength(100)]
        public string? FrameMaterial { get; set; }

        [Column("price")]
        [Required]
        public decimal Price { get; set; }

        [Column("sku")]
        [Required, MaxLength(100)]
        public string Sku { get; set; } = string.Empty;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        // Navigation
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        public virtual Inventory? Inventory { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}