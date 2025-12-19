namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a pickup collection for an order, including the pickup date
/// and whether delivery is included and the location for the pickup
/// </summary>
public class PickUp
{
    /// <summary>
    /// Represents a unique Id for each pickup
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the date of the pickup
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Represents whether delivery is included for the pickup
    /// </summary>
    public bool IsDelivery { get; set; }
    /// <summary>
    /// Represents the location of the pickup
    /// </summary>
    public string Location { get; set; }


    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public PickUp(int id, DateTime date, bool isDelivery, string location)
    {
        Id = id;
        Date = date;
        IsDelivery = isDelivery;
        Location = location;
    }

    /// <summary>
    /// Constructor used for adding a 
    /// new pickup when Id is not known. 
    /// </summary>
    public PickUp(DateTime date, bool isDelivery, string location)
    {
        Date = date;
        IsDelivery = isDelivery;
        Location = location;
    }

    /// <summary>
    /// Constructor used when creating an instance
    /// from the datasource and the pickup is a foreign key
    /// </summary>
    public PickUp(int id)
    {
        Id = id;
    }
}
