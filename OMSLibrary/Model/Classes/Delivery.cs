using OrderManagerLibrary.Model.Interfaces;
namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a delivery of an order to a customer's neighborhood.
/// Used when an order is delivered instead of picked up.
/// </summary>
public class Delivery : ICollectionType
{
    public int CollectionId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int OrderId { get; set; }
    public string Neighborhood { get; set; }

    public Delivery(int collectionId, DateTime collectionDate, int orderId, string neighborhood)
    {
        CollectionId = collectionId;
        CollectionDate = collectionDate;
        OrderId = orderId;
        Neighborhood = neighborhood;
    }

    /// <summary>
    /// Constructor used when creating a new Delivery before saving to the database.
    /// OrderId is known (related to an existing order).
    /// </summary>
    public Delivery(DateTime collectionDate, int orderId, string neighborhood)
    {
        CollectionDate = collectionDate;
        OrderId = orderId;
        Neighborhood = neighborhood;
    }
    /// <summary>
    /// Constructor used when only scheduling a delivery without linking it to an order yet.
    /// Useful for early planning or draft deliveries.
    /// </summary>
    public Delivery(DateTime collectionDate, string neighborhood)
    {
        CollectionDate = collectionDate;
        Neighborhood = neighborhood;
    }
}
