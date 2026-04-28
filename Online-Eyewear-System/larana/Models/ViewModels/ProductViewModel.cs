using OpticalStore.WebApplication.Models;

namespace OpticalStore.WebApplication.Models.ViewModels
{
    // ── Product ─────────────────────────────────────────────────────────────

    public class StockCheckResult
    {
        public bool Found { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class ProductListViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public string? Keyword { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int TotalFound => Products.Count;
        public StockCheckResult? StockResult { get; set; }
        public string? CheckedSku { get; set; }
    }

    public class ProductDetailViewModel
    {
        public Product Product { get; set; } = new();
        public List<ProductVariant> Variants { get; set; } = new();
    }

    // ── Checkout form ────────────────────────────────────────────────────────

    public class CheckoutViewModel
    {
        public string ShippingAddress { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string OrderType { get; set; } = "standard";
        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total => CartItems.Sum(c => (c.Variant?.Price ?? 0) * c.Quantity);
    }
}
