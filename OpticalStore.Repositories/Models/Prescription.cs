using Mysqlx.Crud;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalStore.Repositories.Models
{
   // File: OpticalStore.Repositories/Models/Prescription.cs
    [Table("prescriptions")]
    public class Prescription
    {
    [Key]
    [Column("prescription_id")]
    public int PrescriptionId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("od_sphere")]
    public decimal? OdSphere { get; set; }

    [Column("od_cylinder")]
    public decimal? OdCylinder { get; set; }

    [Column("od_axis")]
    public int? OdAxis { get; set; }  // Đổi từ short? sang int?

    [Column("os_sphere")]
    public decimal? OsSphere { get; set; }

    [Column("os_cylinder")]
    public decimal? OsCylinder { get; set; }

    [Column("os_axis")]
    public int? OsAxis { get; set; }  // Đổi từ short? sang int?

    [Column("pd")]
    public decimal? Pd { get; set; }

    [Column("note")]
    public string Note { get; set; }

    [Column("verified_by")]
    public int? VerifiedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    }
}