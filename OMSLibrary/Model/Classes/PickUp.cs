using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
public class PickUp : IPickUp
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsDelivery { get; set; }
    public string Location { get; set; }

    public PickUp(int id, DateTime date, bool isDelivery, string location)
    {
        Id = id;
        Date = date;
        IsDelivery = isDelivery;
        Location = location;
    }

    public PickUp(DateTime date, bool isDelivery, string location)
    {
        Date = date;
        IsDelivery = isDelivery;
        Location = location;
    }
}
