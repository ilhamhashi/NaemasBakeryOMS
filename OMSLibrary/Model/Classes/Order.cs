using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a customer order in the bakery system. 
/// Contains information such as order date, status, and customer reference.
/// </summary>
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public int CustomerId { get; set; }

    /// <summary>
    /// Constructor used when retrieving an existing order from the database.
    /// OrderId is already known and assigned by the database.
    /// </summary>
    public Order(int orderId, DateTime orderDate, OrderStatus status, int customerId)
    {
        OrderId = orderId;
        OrderDate = orderDate;
        Status = status;
        CustomerId = customerId;
    }
    // <summary>
    /// Constructor used when creating a new order before saving it to the database.
    /// OrderId will be generated automatically when inserted.
    /// </summary>

    public Order(DateTime orderDate, OrderStatus status, int customerId)
    {
        OrderDate = orderDate;
        Status = status;
        CustomerId = customerId;
    }
}
