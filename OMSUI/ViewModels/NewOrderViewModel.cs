using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Services;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;
public class NewOrderViewModel : ViewModel
{
    private readonly IOrderService _orderservice;

    private ObservableCollection<IPaymentMethod> _paymentMethods { get; } = [];
    private ObservableCollection<Payment> _payments { get; } = [];
    private ObservableCollection<OrderLine> OrderLines { get; } = [];
    private ObservableCollection<Product> _products { get; }
    private ObservableCollection<Customer> _customers { get; }
    public ICollectionView ProductsCollectionView { get; }
    public ICollectionView CustomersCollectionView { get; }
    public ICollectionView OrderLinesCollectionView { get; }

    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    private Product selectedProduct;
    private ICustomer selectedCustomer;
    private IPaymentMethod? selectedPaymentMethod;
    private DateTime pickUpDateTime = DateTime.Today.AddDays(1);
    private OrderLine? selectedOrderLine;
    private bool isDelivery = false;
    private string location = "Store";
    private string noteText = string.Empty;
    private decimal runningTotal;
    private decimal discountTotal;
    private decimal paymentAmount;
    private decimal outstandingAmount;
    private decimal priceIncreaseAmount;

    public ICommand CreateOrderCommand { get; }
    public ICommand AddPaymentCommand {  get; }
    public ICommand AddProductToOrderCommand {  get; }

    public ICommand IncreaseQuantityCommand { get; }
    public ICommand DecreaseQuantityCommand { get; }
    public ICommand AddDiscountCommand { get; }
    public ICommand AddPriceIncreaseCommand { get; }

    public ICommand NavigateToNoteViewCommand { get; }
    public ICommand NavigateToPriceChangeViewCommand { get; }
    public ICommand NavigateToPaymentViewCommand { get; }
    public ICommand NavigateToDeliveryViewCommand { get; }

    private bool CanAddNewOrder() => true; 
    private bool CanCancelNewOrder() => true;
    private bool CanContinueToPayment() => true;
    private bool CanGoBackToOrderDetails() => true;
    private bool CanAddToOrder() => true;
    private bool CanSelectProduct() => true;
    private bool CanAddPaymentToOrder() => SelectedPaymentMethod != null && PaymentAmount > 0;

    public Product SelectedProduct
    {
        get { return selectedProduct; }
        set { selectedProduct = value; OnPropertyChanged(); }
    }

    public ICustomer SelectedCustomer
    {
        get { return selectedCustomer; }
        set { selectedCustomer = value; OnPropertyChanged(); }
    }

    public IPaymentMethod? SelectedPaymentMethod
    {
        get { return selectedPaymentMethod; }
        set { selectedPaymentMethod = value; OnPropertyChanged(); }
    }

    public DateTime PickUpDateTime
    {
        get { return pickUpDateTime; }
        set { pickUpDateTime = value; OnPropertyChanged(); }
    }

    public DateTime PickUpDate
    {
        get { return PickUpDateTime; }
        set { PickUpDateTime = value.Date.Add(PickUpDateTime.TimeOfDay); OnPropertyChanged(); }
    }

    public DateTime PickUpTime
    {
        get { return PickUpDateTime; }
        set { PickUpDateTime = PickUpDateTime.Date.Add(value.TimeOfDay); OnPropertyChanged(); }
    }

    public OrderLine? SelectedOrderLine
    {
        get { return selectedOrderLine; }
        set { selectedOrderLine = value; OnPropertyChanged(); }
    }

    public bool IsDelivery
    {
        get { return isDelivery; }
        set { isDelivery = value; OnPropertyChanged(); }
    }

    public string Location
    {
        get { return location; }
        set { location = value; OnPropertyChanged(); }
    }
        public string NoteText
    {
        get { return noteText; }
        set { noteText = value; OnPropertyChanged(); }
    }

	public decimal RunningTotal
	{
		get { return runningTotal; }
		set { runningTotal = value; OnPropertyChanged(); }
	}

    public decimal DiscountTotal
    {
        get { return discountTotal; }
        set { discountTotal = value; OnPropertyChanged(); }
    }

    public decimal PaymentAmount
    {
        get { return paymentAmount; }
        set { paymentAmount = value; OnPropertyChanged(); }
    }

    public decimal OutstandingAmount
    {
        get { return outstandingAmount; }
        set { outstandingAmount = value; OnPropertyChanged(); }
    }

    public decimal PriceIncreaseAmount
    {
        get { return priceIncreaseAmount; }
        set { priceIncreaseAmount = value; OnPropertyChanged(); }
    }

    public NewOrderViewModel(IOrderService orderService, IProductService productService, ICustomerService customerService, INavigationService navigationService)
    {
        _orderservice = orderService;
        _products = new ObservableCollection<Product>(productService.GetAllProducts());
        _customers = new ObservableCollection<Customer>(customerService.GetAllCustomers());
        ProductsCollectionView = CollectionViewSource.GetDefaultView(_products);
        CustomersCollectionView = CollectionViewSource.GetDefaultView(_customers);
        OrderLinesCollectionView = CollectionViewSource.GetDefaultView(OrderLines);
        Navigation = navigationService;
        Navigation.NavigateToNested<NoteViewModel>();

        NavigateToNoteViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<NoteViewModel>(); }, canExecute => true);

        NavigateToPriceChangeViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<PriceChangeViewModel>(); }, canExecute => true);


        NavigateToDeliveryViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<DeliveryViewModel>(); }, canExecute => true);

        NavigateToPaymentViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<PaymentViewModel>(); }, canExecute => true);

        
        CreateOrderCommand = new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
        AddPaymentCommand = new RelayCommand(execute => AddPaymentToOrder(), canExecute => CanAddPaymentToOrder());
        AddProductToOrderCommand = new RelayCommand((param) => AddProductToOrder(param), canExecute => CanAddToOrder());
        IncreaseQuantityCommand = new RelayCommand((param) => IncreaseQuantity(param), canExecute => true);
        DecreaseQuantityCommand = new RelayCommand((param) => DecreaseQuantity(param), canExecute => true);
        AddDiscountCommand = new RelayCommand(execute => AddDiscountToOrderLine(), canExecute => true);
        AddPriceIncreaseCommand = new RelayCommand(execute => AddPriceIncreaseToOrderLine(), canExecute => true);
    }

    private void AddNewOrder()
    {
        Customer customer = new Customer(1, "FirstName", "LastName", "Phone");
        PickUp pickUp = new PickUp(PickUpDateTime, IsDelivery, Location);
        Note note = new Note(NoteText);
        Order newOrder = new(DateTime.Now, OrderStatus.Draft, customer, pickUp, note);
        if (outstandingAmount == 0)
        {
            newOrder.Status = OrderStatus.FullyPaid;
        }

        MessageBoxResult result = MessageBox.Show($"Please confirm order for {customer.FirstName} {customer.LastName}?",
                                  "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            newOrder = _orderservice.CreateOrder(newOrder, [.. OrderLines], [.. _payments]);

            MessageBox.Show($"Order {newOrder.Id} has been created!", "Completed",
                       MessageBoxButton.OK, MessageBoxImage.Information);

            ResetFieldsAfterOrderCompletion();
        }
        
    }

    private void ResetFieldsAfterOrderCompletion()
    {
        OrderLines.Clear();
        SelectedOrderLine = null;
        SelectedPaymentMethod = null;
        _payments.Clear();
        PaymentAmount = decimal.Zero;
        NoteText = string.Empty;
        PickUpDateTime = DateTime.Today.AddDays(1);
        isDelivery = false;
        Location = "Store";
        PriceIncreaseAmount = decimal.Zero;        
    }

    private void AddProductToOrder(object rowData)
    {
        Product product = rowData as Product;
        OrderLine newOrderLine = new(product, 1, product.Price, 0, product.Taste.Name, product.Size.Name);
        ResetLineNumber();
        newOrderLine.LineNumber = OrderLines.Count() + 1;
        OrderLines?.Add(newOrderLine);
        UpdateRunningTotal(); UpdateDiscountTotal(); UpdateOutstandingAmount();
    }

    private void ResetLineNumber()
    {
        int i = 1;
        foreach (var orderline in OrderLines)
        {
            orderline.LineNumber = i;
            i++;
        }
    }

    private void UpdateRunningTotal()
    {
        RunningTotal = (OrderLines.Sum(ol => ol.Price * ol.Quantity));
    }

    private void UpdateDiscountTotal()
    {
        DiscountTotal = (OrderLines.Sum(ol => ol.Discount * ol.Quantity));
    }

    private void IncreaseQuantity(object rowOrderLine)
    {
        (rowOrderLine as OrderLine).IncreaseQuantity();
        UpdateRunningTotal(); UpdateDiscountTotal(); UpdateOutstandingAmount();
        OrderLinesCollectionView.Refresh();
    }

    private void DecreaseQuantity(object rowOrderLine)
    {
        OrderLine orderline = rowOrderLine as OrderLine;
        if (orderline.Quantity > 1)
        {
            orderline.DecreaseQuantity();
        }
        else
        {
            OrderLines.Remove(orderline);
            ResetLineNumber();
        }
        UpdateRunningTotal(); UpdateDiscountTotal();
        OrderLinesCollectionView.Refresh();
    }

    private void AddDiscountToOrderLine()
    {
        SelectedOrderLine.ApplyDiscount();
        UpdateRunningTotal(); UpdateDiscountTotal();
        OrderLinesCollectionView.Refresh();
    }

    private void AddPriceIncreaseToOrderLine()
    {
        SelectedOrderLine.IncreasePrice(PriceIncreaseAmount);
        UpdateRunningTotal();
        OrderLinesCollectionView.Refresh();
    }

    private void AddPaymentToOrder()
    {
        Payment newPayment = new(PaymentAmount, DateTime.Now, SelectedPaymentMethod);
        _payments.Add(newPayment);
        UpdateOutstandingAmount();

        SelectedPaymentMethod = null;
        PaymentAmount = decimal.Zero;
    }

    private void UpdateOutstandingAmount()
    {
        decimal totalPaid = _payments.Sum(p => p.Amount);
        OutstandingAmount = RunningTotal - totalPaid;
    }

}
