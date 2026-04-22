using Microsoft.EntityFrameworkCore;
using OpticalStore.Repositories.Interfaces;
using OpticalStore.Repositories.Models;

namespace OpticalStore.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly OpticalStoreDbContext _context;

        public PrescriptionRepository(OpticalStoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePrescriptionAsync(Prescription prescription)
        {
            prescription.CreatedAt = DateTime.Now;
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            
            // Trả về ID của prescription vừa tạo
            return prescription.PrescriptionId;
        }

        public async Task<List<Prescription>> GetPrescriptionsByUserIdAsync(int userId)
        {
            return await _context.Prescriptions
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}