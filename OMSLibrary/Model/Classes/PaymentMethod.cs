namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents the paymentmethod used 
/// when registering a payment on an order
/// </summary>
public class PaymentMethod
{
    /// <summary>
    /// Represents a unique Id for each paymentmethod
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the name of the paymentmethod
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public PaymentMethod(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Constructor used when creating an instance
    /// from the datasource and the paymentmethod is a foreign key
    /// </summary>
    public PaymentMethod(int id)
    {
        Id = id;
    }
}    
