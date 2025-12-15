using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Services;
public class OrderService : IOrderService
{
    private readonly IDataAccess _db;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderLine> _orderLineRepository;
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<PickUp> _pickUpRepository;
    private readonly IRepository<Note> _noteRepository;
    private readonly IRepository<Customer> _customerRepository;

    public OrderService(IDataAccess dataAccess, IRepository<Order> orderRepository,
                        IRepository<OrderLine> orderLineRepository, IRepository<Payment> paymentRepository,
                        IRepository<Note> noteRepository, IRepository<PickUp> pickUpRepository, 
                        IRepository<Customer> customerRepository)
    {
        _db = dataAccess;
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
        _paymentRepository = paymentRepository;
        _noteRepository = noteRepository;
        _pickUpRepository = pickUpRepository;
        _customerRepository = customerRepository;
    }

    public IEnumerable<Order> GetAllOrders()
    {
        var orders = _orderRepository.GetAll();

        foreach (var order in orders)
        {
            order.Customer = (_customerRepository as CustomerRepository).GetById(order.CustomerId);
            order.PickUp = (_pickUpRepository as PickUpRepository).GetById(order.PickUpId);
            order.Note = (_noteRepository as NoteRepository).GetById(order.NoteId);
        }
        return orders;
    }

    public IEnumerable<Order> GetUpcomingOrders()
    {
        var upcomingOrders = (_orderRepository as OrderRepository).GetUpcomingOrders();
        foreach (var order in upcomingOrders)
        {
            order.Customer = (_customerRepository as CustomerRepository).GetById(order.CustomerId);
            order.PickUp = (_pickUpRepository as PickUpRepository).GetById(order.PickUpId);
            order.Note = (_noteRepository as NoteRepository).GetById(order.NoteId);
        }
        return upcomingOrders;
    }
    public IEnumerable<Order> GetPendingPaymentOrders()
    {
        var pendingPaymentOrders = (_orderRepository as OrderRepository).GetPendingPaymentOrders();
        foreach (var order in pendingPaymentOrders)
        {
            order.Customer = (_customerRepository as CustomerRepository).GetById(order.CustomerId);
            order.PickUp = (_pickUpRepository as PickUpRepository).GetById(order.PickUpId);
        }
        return pendingPaymentOrders;
    }

    public void CreateOrder(Order order, List<OrderLine> orderLines, List<Payment> payments)
    {
        using (SqlConnection connection = _db.GetConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {

                    order.PickUp.Id = _pickUpRepository.Insert((order.PickUp as PickUp));
                    order.Note.Id = _noteRepository.Insert((order.Note as Note));
                    order.Id = _orderRepository.Insert(order);

                    // Opret ordrelinjer
                    foreach (var line in orderLines)
                    {
                        line.OrderId = order.Id; // Sæt OrderId for ordrelinjen
                        _orderLineRepository.Insert(line);
                    }

                    foreach (var payment in payments)
                    {
                        payment.OrderId = order.Id;
                        _paymentRepository.Insert(payment);
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
}