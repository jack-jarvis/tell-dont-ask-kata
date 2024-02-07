using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Main.Service;
using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        public decimal Total => Items.Sum(x => x.TaxedAmount);
        public string Currency { get; private init; }
        public IList<OrderItem> Items { get; private init; }
        public decimal Tax => Items.Sum(x => x.Tax);
        public OrderStatus Status { get; set; }
        public int Id { get; init; }

        public static Order CreateEmpty()
        {
            return new Order
            {
                Status = OrderStatus.Created,
                Items = new List<OrderItem>(),
                Currency = "EUR"
            };
        }
        
        public void ProcessApprovalRequest(OrderApprovalRequest request)
        {
            if (Status == OrderStatus.Shipped)
            {
                throw new ShippedOrdersCannotBeChangedException();
            }

            if (request.Approved && Status == OrderStatus.Rejected)
            {
                throw new RejectedOrderCannotBeApprovedException();
            }

            if (!request.Approved && Status == OrderStatus.Approved)
            {
                throw new ApprovedOrderCannotBeRejectedException();
            }

            Status = request.Approved ? OrderStatus.Approved : OrderStatus.Rejected;
        }

        public void AddItem(Product product, int quantity)
        {
            var orderItem = new OrderItem(product, quantity);
            Items.Add(orderItem);
        }

        public void Ship(IShipmentService shipmentService)
        {
            if (Status is OrderStatus.Created or OrderStatus.Rejected)
            {
                throw new OrderCannotBeShippedException();
            }

            if (Status == OrderStatus.Shipped)
            {
                throw new OrderCannotBeShippedTwiceException();
            }

            shipmentService.Ship(this);

            Status = OrderStatus.Shipped;
        }
    }
}
