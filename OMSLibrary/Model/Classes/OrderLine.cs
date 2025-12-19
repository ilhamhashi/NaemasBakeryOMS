namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a line in an order
/// </summary>
public class OrderLine
{   
    /// <summary>
    /// Represents the product that was sold 
    /// in the OrderLine
    /// </summary>
    public Product Product { get; set; }
    /// <summary>
    /// Represents the order that the
    /// OrderLine belongs to
    /// </summary>
    public Order Order { get; set; }
    /// <summary>
    /// Represents the numbered placement of the OrderLine 
    /// in relation to other OrderLines in the same order
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// Represents the quantity sold 
    /// of the product in the orderline
    /// </summary>
    public int Quantity { get; set; }
    /// <summary>
    /// Represents the price of the product sold
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Represents the amount of Discount 
    /// applied on the product price
    /// </summary>
    public decimal Discount { get; set; }
    /// <summary>
    /// Represents the size of the product sold
    /// </summary>
    public string Size { get; set; }
    /// <summary>
    /// Represents the taste of the product sold
    /// </summary>
    public string Taste { get; set; }


    /// <summary>
    /// Constructor used for adding a OrderLine while 
    /// creating the order, but the orderId is not known. 
    /// </summary>
    public OrderLine(Product product, int quantity, decimal price, decimal discount, string taste, string size)
    {
        Product = product;
        Quantity = quantity;
        Price = price;
        Discount = discount;
        Taste = taste;
        Size = size;
    }

    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public OrderLine(Product product, Order order, int lineNumber, int quantity, decimal price, decimal discount, string size, string taste)
    {
        Product = product;
        Order = order;
        LineNumber = lineNumber;        
        Quantity = quantity;
        Price = price;
        Discount = discount;
        Size = size;
        Taste = taste;
    }

    /// <summary>
    /// Reduces the price by the discount amount 
    /// </summary>
    public void ApplyDiscount()
    {
        Price -= Discount;
    }

    /// <summary>
    /// Increases orderline quantity by 1 
    /// </summary>
    public void IncreaseQuantity()
    {
        Quantity++;
    }

    /// <summary>
    /// Reduces orderline quantity by 1 
    /// </summary>
    public void DecreaseQuantity()
    {
        Quantity--;
    }
}
