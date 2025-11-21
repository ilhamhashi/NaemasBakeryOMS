using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
using System.Data;

namespace OrderManagerLibrary.Service;
public class OrderService
{
    private readonly IDbConnection _connection;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderLine> _orderLineRepository;
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<ICollectionType> _collectionRepository;
    private readonly IRepository<Note> _noteRepository;

    public OrderService(IDbConnection connection, IRepository<Order> orderRepository, IRepository<OrderLine> orderLineRepository, IRepository<Payment> paymentRepository, IRepository<ICollectionType> collectionRepository, IRepository<Note> noteRepository)
    {
        _connection = connection;
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
        _paymentRepository = paymentRepository;
        _collectionRepository = collectionRepository;
        _noteRepository = noteRepository;
    }

    public void CreateOrder(Order order, List<OrderLine> orderLines, List<IPaymentMethod> paymentMethods, 
        List<Payment> payments, ICollectionType collection, INote note)
    {
        using (var transaction = _orderRepository.Connection.BeginTransaction())
        {
            try
            {
                int i = 0;
                // Opret ordre
                _orderRepository.Add(order);

                // Opret ordrelinjer
                foreach (var line in orderLines)
                {
                    line.OrderId = order.OrderId; // Sæt OrderId for ordrelinjen
                    _orderLineRepository.Add(line);
                }

                foreach (var payment in payments)
                {
                    payment.OrderId = order.OrderId;
                    payment.PaymentMethodId = paymentMethods[i].PaymentMethodId;
                    _paymentRepository.Insert(payment);
                    i++;
                }

                collection.OrderId = order.OrderId;
                _collectionRepository.Insert(collection);

                note.OrderId = note.OrderId;
                _noteRepository.Insert(note);

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

//sæt ordrestatus (draft??)
//