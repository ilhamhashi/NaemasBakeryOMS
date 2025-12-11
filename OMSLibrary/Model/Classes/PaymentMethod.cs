using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a specific payment method
/// used to process payments for an order.
/// </summary>
public class PaymentMethod : IPaymentMethod
{
    public int Id { get; set; }
    public string Name { get; set; }

    public PaymentMethod(int id, string name)
    {
        Id = id;
        Name = name;
    }
}    
