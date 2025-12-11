using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUpcomingOrders();
        IEnumerable<Order> GetPendingPaymentOrders();
        IEnumerable<Product> ViewProductCatalogue();
        void CreateOrder(Order order, List<OrderLine> orderLines,List<Payment> payments, IPickUp collection, INote? note);
    }
}