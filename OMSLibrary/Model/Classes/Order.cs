using OrderManagerLibrary.Model.Interfaces;
namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents an order from a customer in the bakery. 
/// Contains information such as order date, status, and customer reference.
/// </summary>
public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
    public Customer Customer { get; set; }
    public IPickUp PickUp { get; set; }
    public INote Note { get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public List<Payment> Payments { get; set; }

    public int CustomerId { get; set; }
    public string CustomerFullName { get; set; }
    public decimal OutstandingAmount { get; set; }

    public Order(int id, DateTime date, OrderStatus status, int customerId, int pickUpId, int noteId)
    {
        Id = id;
        Date = date;
        Status = status;
        Customer.Id = customerId;
        PickUp.Id = pickUpId;
        Note.Id = noteId;
    }

    /// <summary>
    /// Constructor used when retrieving an existing order from the database.
    /// OrderId is already known and assigned by the database.
    /// </summary>
    public Order(int orderId, DateTime orderDate, OrderStatus status, int customerId)
    {
        Id = orderId;
        Date = orderDate;
        Status = status;
        CustomerId = customerId;
    }

    // <summary>
    /// Constructor used when creating a new order before saving it to the database.
    /// OrderId will be generated automatically when inserted.
    /// </summary>

    public Order(DateTime orderDate, OrderStatus status, int customerId)
    {
        Date = orderDate;
        Status = status;
        CustomerId = customerId;
    }

    public Order(int orderId, DateTime orderDate, string customerFullName, DateTime pickUp, OrderStatus status)
    {
        Id = orderId;
        Date = orderDate;
        CustomerFullName = customerFullName;
        PickUp.Date = pickUp;
        Status = status;
    }

    public Order(int orderId, string customerFullName, decimal outstandingAmount)
    {
        Id = orderId;
        CustomerFullName = customerFullName;
        OutstandingAmount = outstandingAmount;
    }
}
