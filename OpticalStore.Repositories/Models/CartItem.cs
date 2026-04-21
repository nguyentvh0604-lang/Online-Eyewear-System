using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("cart")]
    public class CartItem
    {
        [Key]
        [Column("cart_id")]
        public int CartId { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("variant_id")]
        [Required]
        public int VariantId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; } = 1;

        [Column("added_at")]
        public DateTime AddedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [ForeignKey("VariantId")]
        public virtual ProductVariant? Variant { get; set; }
    }
}