using OpticalStore.WebApplication.Models.ViewModels;

namespace OpticalStore.WebApplication.Services
{
    public class PaymentService : IPaymentService
    {
        public bool ProcessPayment(PaymentViewModel model)
        {
            if (model.TotalAmount <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(model.PaymentMethod))
                return false;

            return true;
        }
    }
}
