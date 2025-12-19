using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Size = OrderManagerLibrary.Model.Classes.Size;

namespace OrderManagerDesktopUI.ViewModels;

public class ProductsViewModel : ViewModelBase
{
    private readonly IProductService _productService;
    private ObservableCollection<Product> _products { get; }
    public ICollectionView ProductsCollectionView { get; }
    public ObservableCollection<Size> SizeOptions { get; } = [];
    public ObservableCollection<Taste> TasteOptions { get; } = [];
    private ObservableCollection<Size> _availableSizes;
    public ObservableCollection<Size> AvailableSizes
    {
        get { return _availableSizes; }
        set { _availableSizes = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Taste> _availableTastes;
    public ObservableCollection<Taste> AvailableTastes
    {
        get { return _availableTastes; }
        set { _availableTastes = value; OnPropertyChanged(); }
    }

    private Product _selectedProduct;
    public Product SelectedProduct
    {
        get { return _selectedProduct; }
        set { _selectedProduct = value; OnPropertyChanged(); }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; OnPropertyChanged(); }
    }

    private string _description;
    public string Description
    {
        get { return _description; }
        set { _description = value; OnPropertyChanged(); }
    }

    private decimal _price;
    public decimal Price
    {
        get { return _price; }
        set { _price = value; OnPropertyChanged(); }
    }

    private Size _selectedSize;
    public Size SelectedSize
    {
        get { return _selectedSize; }
        set { _selectedSize = value; OnPropertyChanged(); }
    }

    private Taste _selectedTaste;
    public Taste SelectedTaste
    { 
        get { return _selectedTaste; }
        set { _selectedTaste = value; OnPropertyChanged(); }
    }

    private string searchTerm = string.Empty;
    public string SearchTerm
    {
        get { return searchTerm; }
        set
        {
            searchTerm = value;
            OnPropertyChanged(nameof(ProductFilter));
            ProductsCollectionView.Refresh();
        }
    }

    public ICommand CreateProductCommand { get; }
    public ICommand UpdateProductCommand { get; }
    public ICommand DeleteProductCommand { get; }
    public ICommand AddSizeToProductCommand { get; }
    public ICommand AddTasteToProductCommand { get; }
    public ICommand RemoveSizeFromProductCommand { get; }
    public ICommand RemoveTasteFromProductCommand { get; }

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
        _products = new ObservableCollection<Product>(_productService.GetAllProducts().Where(p => !p.IsArchived));
        ProductsCollectionView = CollectionViewSource.GetDefaultView(_products);
        ProductsCollectionView.Filter = ProductFilter;
        AvailableSizes = new ObservableCollection<Size>(_productService.GetAllSizes());
        AvailableTastes = new ObservableCollection<Taste>(_productService.GetAllTastes());
        CreateProductCommand = new RelayCommand(execute => CreateProduct(), canExecute => true);
        UpdateProductCommand = new RelayCommand(execute => UpdateProduct(), canExecute => SelectedProduct != null);
        DeleteProductCommand = new RelayCommand((param) => DeleteProduct(param), canExecute => true);
        AddSizeToProductCommand = new RelayCommand(execute => AddSizeToProduct(), canExecute => SelectedSize != null);
        AddTasteToProductCommand = new RelayCommand(execute => AddTasteToProduct(), canExecute => SelectedTaste != null);
        RemoveSizeFromProductCommand = new RelayCommand(execute => RemoveSizeFromProduct(), canExecute => SelectedSize != null);
        RemoveTasteFromProductCommand = new RelayCommand(execute => RemoveTasteFromProduct(), canExecute => SelectedTaste != null);
    }

    private void AddSizeToProduct()
    {        
        SizeOptions.Add(SelectedSize);
        AvailableSizes.Remove(SelectedSize);
        SelectedSize = null;
    }

    private void AddTasteToProduct()
    {
        TasteOptions.Add(SelectedTaste);
        AvailableTastes.Remove(SelectedTaste);
        SelectedTaste = null;
    }

    private void RemoveSizeFromProduct()
    {        
        AvailableSizes.Add(SelectedSize);
        SizeOptions.Remove(SelectedSize);
        SelectedSize = null;
    }

    private void RemoveTasteFromProduct()
    {
        AvailableTastes.Add(SelectedTaste);
        TasteOptions.Remove(SelectedTaste);
        SelectedTaste = null;
    }

    private bool ProductFilter(object obj)
    {
        if (obj is Product product)
        {
            return product.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                   product.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                   product.Id.ToString().Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
        }
        return false;
    }

    private void CreateProduct()
    {
        var product = new Product(Name, Description, Price, false, [.. SizeOptions], [.. TasteOptions]);
        _productService.CreateProduct(product);
        _products.Add(product);
        MessageBox.Show($"You have succesfully created {product.Name}.");
        ResetFields();
    }

    private void ResetFields()
    {
        Name = string.Empty;
        Description = string.Empty;
        Price = 0;
        SelectedSize = null;
        SelectedTaste = null;
        SelectedProduct = null;
        SizeOptions.Clear();
        TasteOptions.Clear();
        AvailableSizes = new ObservableCollection<Size>(_productService.GetAllSizes());
        AvailableTastes = new ObservableCollection<Taste>(_productService.GetAllTastes());
    }

    private void UpdateProduct()
    {
        if (SelectedProduct != null)
        {
            SelectedProduct.SizeOptions = [.. SizeOptions];
            SelectedProduct.TasteOptions = [.. TasteOptions];
            _productService.UpdateProduct(SelectedProduct);
            ProductsCollectionView.Refresh();
            ResetFields();
        }
    }

    private void DeleteProduct(object rowData)
    {
        if (rowData != null)
        {
            var product = rowData as Product;
            MessageBoxResult result = MessageBox.Show($"Do you want to archive {product.Name}?",
                                      "OrderManager", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                product.IsArchived = true;
                _productService.UpdateProduct(product);
                _products.Remove(product);

                MessageBox.Show($"{product.Name} is removed.", "OrderManager",
                           MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}