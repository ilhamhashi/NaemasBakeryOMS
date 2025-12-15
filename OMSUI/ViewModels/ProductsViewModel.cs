using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;

public class ProductsViewModel : ViewModel
{
    private readonly IProductService _productService;

    public ObservableCollection<Product> Products { get; set; }
    public ICollectionView ProductsView { get; set; }

    private Product? _selectedProduct;
    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set { _selectedProduct = value; OnPropertyChanged(); }
    }

    // OPRET NYT PRODUKT FELTER
    public string NewName { get; set; }
    public string NewDescription { get; set; }
    public decimal NewPrice { get; set; }

    // Filter
    private string _filterText;
    public string FilterText
    {
        get => _filterText;
        set
        {
            _filterText = value;
            OnPropertyChanged();
            ProductsView.Refresh();   // Filter når tekst ændres
        }
    }

    // Commands
    public ICommand CreateCommand { get; set; }
    public ICommand UpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;

        // Hent produkter
        Products = new ObservableCollection<Product>(_productService.GetAllProducts());

        // Setup filter
        ProductsView = CollectionViewSource.GetDefaultView(Products);
        ProductsView.Filter = FilterProducts;

        // Commands
        CreateCommand = new RelayCommand(o => CreateProduct(), o => true);
        UpdateCommand = new RelayCommand(o => UpdateProduct(), o => SelectedProduct != null);
        DeleteCommand = new RelayCommand(o => DeleteProduct(), o => SelectedProduct != null);
    }


    private bool FilterProducts(object obj)
    {
        if (obj is not Product p) return false;

        if (string.IsNullOrWhiteSpace(FilterText))
            return true;

        return p.Name.ToLower().Contains(FilterText.ToLower());
    }

    private void CreateProduct()
    {
        var p = new Product(NewName, NewDescription, NewPrice);

        _productService.CreateProduct(p);
        Products.Add(p);

        MessageBox.Show($"You have succesfully created {p.Name}.");

        // Clear fields
        NewName = "";
        NewDescription = "";
        NewPrice = 0;
        OnPropertyChanged(nameof(NewName));
        OnPropertyChanged(nameof(NewDescription));
        OnPropertyChanged(nameof(NewPrice));
    }

    private void UpdateProduct()
    {
        _productService.UpdateProduct(SelectedProduct);
        ProductsView.Refresh();
    }

    private void DeleteProduct()
    {
        _productService.RemoveProduct(SelectedProduct.Id);
        Products.Remove(SelectedProduct);
    }
}