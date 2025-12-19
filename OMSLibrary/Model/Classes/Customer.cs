namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a customer ordering from the bakery
/// </summary>
public class Customer
{
    /// <summary>
    /// Represents a unique Id for each customer
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the firstname of the customer
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Represents the lastname of the customer
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Represents the phone number of the customer
    /// </summary>
    public string PhoneNumber { get; set; }
    /// <summary>
    /// Represents a collection of orders made by the customer
    /// </summary>
    public List<Order> Orders { get; set; } = [];


    /// <summary>
    /// Constructor used for creating a new customer
    /// when Id is not known. 
    /// </summary>
    public Customer(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public Customer(int personId, string firstName, string lastName, string phoneNumber)
    {
        Id = personId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Constructor used when creating an instance
    /// from the datasource and the customer is a foreign key
    /// </summary>
    public Customer(int id)
    {
        Id = id;
    }
}
