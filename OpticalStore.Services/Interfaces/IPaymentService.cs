using OpticalStore.WebApplication.Models.ViewModels;

namespace OpticalStore.WebApplication.Services
{
    public interface IPaymentService
    {
        bool ProcessPayment(PaymentViewModel model);
    }
}
