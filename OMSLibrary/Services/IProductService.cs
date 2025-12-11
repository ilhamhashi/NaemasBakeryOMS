using OrderManagerLibrary.Model.Classes;

namespace OrderManagerLibrary.Services;
public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    void CreateProduct(Product product);
    void UpdateProduct(Product product);
    void RemoveProduct(int id);
}
