namespace OrderManagerLibrary.Model.Classes;
public class ProductTaste
{
    public int TasteId { get; set; }
    public int ProductId { get; set; }

    public ProductTaste(int tasteId, int productId)
    {
        TasteId = tasteId;
        ProductId = productId;
    }
}
