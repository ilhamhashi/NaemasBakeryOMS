namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a single line in an order, including the product,
/// quantity, price, and any discount applied
/// </summary>
public class OrderLine
{
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int LineNumber { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; } = 0;

    /// <summary>
    /// Creates an order line using a product ID and order details
    /// </summary>

    public OrderLine(int productId, int orderId, int lineNumber, int quantity, decimal price, decimal discount)
    {
        ProductId = productId;
        OrderId = orderId;
        LineNumber = lineNumber;
        Quantity = quantity;
        Price = price;
        Discount = discount;
    }

    /// <summary>
    /// Creates an order line using a product object and order details
    /// </summary>
    public OrderLine(Product product, int lineNumber, int quantity, decimal price, decimal discount)
    {
        Product = product;
        LineNumber = lineNumber;
        Quantity = quantity;
        Price = price;
        Discount = discount;
    }

    /// <summary>
    /// Reduces the price by the discount amount 
    /// </summary>
    public void ReducePrice()
    {
        Price -= Discount;
    }
}
