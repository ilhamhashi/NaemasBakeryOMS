namespace OrderManagerLibrary.Model.Interfaces;
public interface IPickUp
{
    int Id { get; set; }
    DateTime Date { get; set; }
    bool IsDelivery { get; set; }
    string Location { get; set; }
}
