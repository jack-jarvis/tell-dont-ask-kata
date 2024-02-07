namespace TellDontAskKata.Main.Domain
{
    public class OrderItem
    {
        public Product Product { get; private init; }
        public int Quantity { get; private set; }
        public decimal TaxedAmount { get; private set; }
        public decimal Tax { get; private set; }

        public static OrderItem ProductOrder(Product product, int quantity)
        {
            var taxedAmount = decimal.Round(product.UnitaryTaxedAmount * quantity, 2, System.MidpointRounding.ToPositiveInfinity);
            var taxAmount = decimal.Round(product.UnitaryTax * quantity, 2, System.MidpointRounding.ToPositiveInfinity);

            return new OrderItem
            {
                Product = product,
                Quantity = quantity,
                Tax = taxAmount,
                TaxedAmount = taxedAmount
            };
        }
    }
}
