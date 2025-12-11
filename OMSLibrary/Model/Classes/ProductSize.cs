namespace OrderManagerLibrary.Model.Classes;
public class ProductSize
{
    public int SizeId { get; set; }
    public int ProductId { get; set; }

    public ProductSize(int sizeId, int productId)
    {
        SizeId = sizeId;
        ProductId = productId;
    }
}
