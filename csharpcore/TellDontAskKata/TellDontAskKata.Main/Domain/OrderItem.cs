namespace TellDontAskKata.Main.Domain
{
    public class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        public decimal TaxedAmount => decimal.Round(Product.UnitaryTaxedAmount * Quantity, 2,
            System.MidpointRounding.ToPositiveInfinity);

        public decimal Tax =>
            decimal.Round(Product.UnitaryTax * Quantity, 2, System.MidpointRounding.ToPositiveInfinity);

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}