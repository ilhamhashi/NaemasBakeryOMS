using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
using System.Data;

namespace OMSDesktopUI.Services;

public class OrderService
{
    
    private readonly IDbConnection _connection;
    private readonly OrderRepository _orderRepository;
    private readonly OrderLineRepository _orderLineRepository;

    public OrderService(IDbConnection connection, IRepository<Order> orderRepository, IRepository<OrderLine> orderLineRepository)
    {
        _connection = connection;
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
    }

    public void CreateOrder(Order order, List<OrderLine> orderLines)
    {
        using (var transaction = _orderRepository.Connection.BeginTransaction())
        {
            try
            {
                // Opret ordre
                _orderRepository.Add(order);

                // Opret ordrelinjer
                foreach (var line in orderLines)
                {
                    line.OrderId = order.OrderId; // Sæt OrderId for ordrelinjen
                    _orderLineRepository.Add(line);
                }

                // Commit transaction
                transaction.Commit();
            }
            catch (Exception)
            {
                // Rul tilbage transaktionen ved fejl
                transaction.Rollback();
                throw; // Re-throw exception to handle it upstream
            }
        }
    }
}
