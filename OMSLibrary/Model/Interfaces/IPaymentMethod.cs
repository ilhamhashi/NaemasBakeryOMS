namespace OrderManagerLibrary.Model.Interfaces;

public interface IPaymentMethod
{
    int Id { get; set; }
    string Name { get; set; }
}
