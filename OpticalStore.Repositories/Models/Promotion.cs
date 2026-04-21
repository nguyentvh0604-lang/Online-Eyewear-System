using Mysqlx.Crud;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("promotions")]
    public class Promotion
    {
        [Key]
        [Column("promotion_id")]
        public int PromotionId { get; set; }

        [Column("name")]
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Column("discount_type")]
        [Required]
        public string DiscountType { get; set; } = string.Empty;

        [Column("discount_value")]
        [Required]
        public decimal DiscountValue { get; set; }

        [Column("min_order_value")]
        public decimal? MinOrderValue { get; set; } = 0;

        [Column("start_date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_by")]
        [Required]
        public int CreatedBy { get; set; }

        // Navigation
        [ForeignKey("CreatedBy")]
        public virtual User? CreatedByUser { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}