namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a product with an id, name, description, and price
/// Also includes a list of Taste and Size objects related to the product.
/// </summary>
public class Product
{
    /// <summary>
    /// Represents a unique Id for each product
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the name of the product
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Represents the description of the product
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Represents the price of the product
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Represents whether the product is archived.
    /// If archived the product is not on any productlist
    /// and only stored for previous order records
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// Represents different tastes
    /// that can be selected for the product
    /// </summary>
    public List<Taste> TasteOptions { get; set; } = [];
    /// <summary>
    /// Represents different sizes
    /// that can be selected for the product
    /// </summary>
    public List<Size> SizeOptions { get; set; } = [];
    /// <summary>
    /// Represents the selected taste for the product
    /// </summary>
    public Taste Taste { get; set; }
    /// <summary>
    /// Represents the selected size for the product
    /// </summary>
    public Size Size { get; set; }


    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public Product(int id, string name, string description, decimal price, bool isArchived)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        IsArchived = isArchived;
    }

    /// <summary>
    /// Constructor used for creating a new product
    /// when Id is not known. 
    /// </summary>
    public Product(string name, string description, decimal price, bool isArchived, List<Size> sizes, List<Taste> tastes)
    {
        Name = name;
        Description = description;
        Price = price;
        IsArchived = isArchived;
        SizeOptions = sizes;
        TasteOptions = tastes;
    }

    /// <summary>
    /// Constructor used when creating an instance
    /// from the datasource and the product is a foreign key
    /// </summary>
    public Product(int id)
    {
        Id = id;
    }
}
