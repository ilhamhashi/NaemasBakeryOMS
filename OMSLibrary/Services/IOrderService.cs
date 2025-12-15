using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetUpcomingOrders();
        IEnumerable<Order> GetPendingPaymentOrders();
        void CreateOrder(Order order, List<OrderLine> orderLines,List<Payment> payments);
    }
}