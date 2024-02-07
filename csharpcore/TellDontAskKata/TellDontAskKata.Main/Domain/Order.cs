using System.Collections.Generic;
using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        public decimal Total { get; set; }
        public string Currency { get; init; }
        public IList<OrderItem> Items { get; init; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; init; }
        
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
    }
}
