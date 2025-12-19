

namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a payment registered on a order
/// </summary>
public class Payment
{
    /// <summary>
    /// Represents a unique Id for each payment
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the date the payment was registered
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Represents the amount registered
    /// </summary>
    public decimal Amount { get; set; }
    /// <summary>
    /// Represents the paymentmethod of the payment
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }
    /// <summary>
    /// Represents the order that the payment was made on
    /// </summary>
    public Order Order { get; set; }

    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public Payment(int id,  DateTime date, decimal amount, PaymentMethod paymentMethod, Order order)
    {
        Id = id;
        Date = date;
        Amount = amount;
        PaymentMethod = paymentMethod;
        Order = order;
    }

    /// <summary>
    /// Constructor used for adding a 
    /// payment when Id is not known. 
    /// </summary>
    public Payment(decimal paymentAmount, DateTime paymentDate, PaymentMethod paymentMethod)
    {
        Amount = paymentAmount;
        Date = paymentDate;
        PaymentMethod = paymentMethod;
    }
}
