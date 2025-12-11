namespace OrderManagerLibrary.Model.Classes;
/// <summary>
/// Represents a product with a name, description, and price
/// used within the order managment system
/// </summary>

public class Product
{

    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }


    public Product(int productId, string name, string description, decimal price)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
    }


    public Product(string name, string description, decimal price)
    {

        Name = name;
        Description = description;
        Price = price;
    }
}
