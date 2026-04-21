using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
    [Table("policies")]
    public class Policy
    {
        [Key]
        [Column("policy_id")]
        public int PolicyId { get; set; }

        [Column("policy_type")]
        [Required]
        public string PolicyType { get; set; } = string.Empty;

        [Column("title")]
        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [Column("content")]
        public string? Content { get; set; }

        [Column("effective_date")]
        [Required]
        public DateTime EffectiveDate { get; set; }

        [Column("created_by")]
        [Required]
        public int CreatedBy { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("CreatedBy")]
        public virtual User? CreatedByUser { get; set; }
        public virtual ICollection<ReturnRequest> ReturnRequests { get; set; } = new List<ReturnRequest>();
    }
}