using OpticalStore.Repositories.Models;

namespace OpticalStore.Repositories.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task<int> CreatePrescriptionAsync(Prescription prescription);
        Task<List<Prescription>> GetPrescriptionsByUserIdAsync(int userId);
    }
}