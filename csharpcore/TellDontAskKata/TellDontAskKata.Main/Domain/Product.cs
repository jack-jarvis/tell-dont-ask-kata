using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class Product
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public Category Category { get; init; }
        
        public decimal UnitaryTax => decimal.Round(Price / 100m * Category.TaxPercentage, 2, System.MidpointRounding.ToPositiveInfinity);

        public decimal UnitaryTaxedAmount => Price + UnitaryTax;
        
        public void AddTo(Order order, int quantity)
        {
            var orderItem = OrderItem.ProductOrder(this, quantity);
            order.Items.Add(orderItem);
        }
        
        
    }
}