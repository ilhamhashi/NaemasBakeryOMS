using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a pickup collection for an order, including the pickup date
/// and the related order information
/// </summary>
public class PickUp : ICollectionType
{

    public int CollectionId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int OrderId { get; set; }

    /// <summary>
    /// Creates a pickup with an ID, a date, and the order it belongs to.
    /// </summary>

    public PickUp(int collectionId, DateTime collectionDate, int orderId)
    {
        CollectionId = collectionId;
        CollectionDate = collectionDate;
        OrderId = orderId;
    }
    /// <summary>
    /// Creates a pickup using only the pickup date.
    /// </summary>

    public PickUp(DateTime collectionDate)
    {
        CollectionDate = collectionDate;
    }
}
