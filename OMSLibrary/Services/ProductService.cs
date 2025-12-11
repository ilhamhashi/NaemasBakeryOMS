using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Services;
public class ProductService : IProductService
{
    private readonly IDataAccess _db;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Size> _sizeRepository;
    private readonly IRepository<Taste> _tasteRepository;

    public ProductService(IDataAccess dataAccess, IRepository<Product> productRepository, 
                          IRepository<Size> sizeRepository, IRepository<Taste> tasteRepository)
    {
        _db = dataAccess;
        _productRepository = productRepository;
        _sizeRepository = sizeRepository;
        _tasteRepository = tasteRepository;
    }

    public IEnumerable<Product> GetAllProducts()
    { 
        var products = _productRepository.GetAll();

        foreach (var product in products)
        {
            product.Sizes.AddRange((_sizeRepository as SizeRepository).GetByProductId(product.Id));
            product.Tastes.AddRange((_tasteRepository as TasteRepository).GetByProductId(product.Id));

        }

        return products;
    }

    public Product? GetProductById(int id) => _productRepository.GetById(id);
    public void CreateProduct(Product product) => _productRepository.Insert(product);
    public void UpdateProduct(Product product) => _productRepository.Update(product);
    public void RemoveProduct(int id) => _productRepository.Delete(id);

}
