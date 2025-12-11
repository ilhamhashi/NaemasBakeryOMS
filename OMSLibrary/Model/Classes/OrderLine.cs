namespace OrderManagerLibrary.Model.Classes;
public class OrderLine
{
    public Product Product { get; set; }
    public Order Order { get; set; }
    public int LineNumber { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }

    public int OrderId { get; set; }

    public OrderLine(int productId, int orderId, int lineNumber, int quantity, decimal price, decimal discount)
    {
        Product.Id = productId;
        OrderId = orderId;
        LineNumber = lineNumber;        
        Quantity = quantity;
        Price = price;
        Discount = discount;
    }

    public OrderLine(Product product, int quantity, decimal price, decimal discount)
    {
        Product = product;
        Quantity = quantity;
        Price = price;
        Discount = discount;
    }

    public void ApplyDiscount()
    {
        Price -= Discount;
    }

    public void IncreasePrice(decimal increaseAmount)
    {
        Price += increaseAmount;
    }

    public void IncreaseQuantity()
    {
        Quantity++;
    }

    public void DecreaseQuantity()
    {
        Quantity--;
    }
}
