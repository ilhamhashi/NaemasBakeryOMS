using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;
public class NewOrderViewModel : ViewModel
{
    private readonly IOrderService _orderservice;

    private Product? selectedProduct;
	private ICustomer selectedCustomer;
	private IPaymentMethod? selectedPaymentMethod;
    private Payment? selectedPayment;
    private OrderStatus orderStatus;
    private int lineNumber;
    private OrderLine? selectedOrderLine;
    private string noteText = "neworderviewmodel";
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

    private DateTime pickUpDateTime;
    private string address;    
    private decimal paymentAmount;
	private bool isDelivery;    
	private decimal? runningTotal;
	private decimal outstandingAmount;
    private decimal? discountTotal;

    public decimal? DiscountTotal
    {
        get { return discountTotal; }
        set { discountTotal = value; OnPropertyChanged(); }
    }



    private ObservableCollection<IPaymentMethod> PaymentMethods {  get; set; } = [];
    private ObservableCollection<Payment> Payments { get; set; } = [];
    public ObservableCollection<OrderLine> OrderLines { get; set; } = [];
    public ICollectionView OrderLinesCollectionView { get; set; }
    public ObservableCollection<Product> Products { get; set;}
    public static ICollectionView? ProductsCollectionView { get; set; }

    public ICommand CreateOrderCommand { get; private set; }
    public ICommand AddPaymentCommand {  get; private set; }
    public ICommand AddProductToOrderCommand {  get; private set; }

    public ICommand IncreaseQuantityCommand { get; private set; }
    public ICommand DecreaseQuantityCommand { get; private set; }
    public ICommand AddDiscountCommand { get; private set; }
    public ICommand AddPriceIncreaseCommand { get; private set; }

    public ICommand NavigateToNoteViewCommand { get; private set; }
    public ICommand NavigateToPriceChangeViewCommand { get; private set; }
    public ICommand NavigateToPaymentViewCommand { get; private set; }
    public ICommand NavigateToDeliveryViewCommand { get; private set; }

    private bool CanAddNewOrder() => true; // Placeholder for actual logic
    private bool CanCancelNewOrder() => true; // Placeholder for actual logic
    private bool CanContinueToPayment() => true; // Placeholder for actual logic
    private bool CanGoBackToOrderDetails() => true; // Placeholder for actual logic
    private bool CanAddToOrder() => true;
    private bool CanSelectProduct() => true; // Placeholder for actual logic
    private bool CanAddPaymentToOrder() => SelectedPaymentMethod != null && PaymentAmount > 0;

   
    public Product? SelectedProduct
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
    public OrderStatus OrderStatus
    {
        get { return orderStatus; }
        set { orderStatus = value; OnPropertyChanged(); }
    }
    public int LineNumber
    {
        get { return lineNumber; }
        set { lineNumber = value; OnPropertyChanged(); }
    }
    public OrderLine? SelectedOrderLine
    {
        get { return selectedOrderLine; }
        set { selectedOrderLine = value; OnPropertyChanged(); }
    }

    public string NoteText
    {
        get { return noteText; }
        set { noteText = value; OnPropertyChanged(); }
    }
    public DateTime PickUpDateTime
    {
        get { return pickUpDateTime; }
        set { pickUpDateTime = value; OnPropertyChanged(); }
    }
    public string Address
    {
        get { return address; }
        set { address = value; OnPropertyChanged(); }
    }
    public decimal PaymentAmount
	{
		get { return paymentAmount; }
		set { paymentAmount = value; OnPropertyChanged(); }
	}

    public Payment? SelectedPayment
    {
        get { return selectedPayment; }
        set { selectedPayment = value; }
    }

    public bool IsDelivery
    {
        get { return isDelivery; }
        set { isDelivery = value; OnPropertyChanged(); }
    }
	public decimal? RunningTotal
	{
		get { return runningTotal; }
		set { runningTotal = value; OnPropertyChanged(); }
	}
   public decimal OutstandingAmount
	{
		get { return outstandingAmount; }
		set { outstandingAmount = value; OnPropertyChanged(); }
	}

    private decimal priceIncreaseAmount;

    public decimal PriceIncreaseAmount
    {
        get { return priceIncreaseAmount; }
        set { priceIncreaseAmount = value; OnPropertyChanged(); }
    }


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
    public NewOrderViewModel(IOrderService orderService, INavigationService navigationService)
    {
        _orderservice = orderService;
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


        Products = new(_orderservice.ViewProductCatalogue());
        ProductsCollectionView = CollectionViewSource.GetDefaultView(Products);

        CreateOrderCommand = new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
        AddPaymentCommand = new RelayCommand(execute => AddPaymentToOrder(), canExecute => CanAddPaymentToOrder());
        AddProductToOrderCommand = new RelayCommand((param) => AddProductToOrder(param), canExecute => CanAddToOrder());
        IncreaseQuantityCommand = new RelayCommand((param) => IncreaseQuantity(param), canExecute => true);
        DecreaseQuantityCommand = new RelayCommand((param) => DecreaseQuantity(param), canExecute => true);
        AddDiscountCommand = new RelayCommand((param) => AddDiscountToOrderLine(param), canExecute => true);
        AddPriceIncreaseCommand = new RelayCommand((param) => AddPriceIncreaseToOrderLine(param), canExecute => true);
    }

    private void AddNewOrder()
    {
        Order newOrder = new(DateTime.Now, OrderStatus.Draft, selectedCustomer.Id);
        if (outstandingAmount == 0)
        {
            newOrder.Status = OrderStatus.FullyPaid;
        }
        IPickUp collection = HandleDelivery();
        INote orderNote = new Note(NoteText);

        _orderservice.CreateOrder(newOrder, [.. OrderLines], [.. Payments], collection, orderNote);

        MessageBox.Show($"Order {newOrder.Id} has been saved succesfully");
        
        ResetFieldsAfterOrderCompletion();
    }

    private void ResetFieldsAfterOrderCompletion()
    {
        OrderLines.Clear();
        SelectedOrderLine = null;
        SelectedPaymentMethod = null;
        Payments.Clear();
        SelectedPayment = null;
        PaymentAmount = decimal.Zero;
        NoteText = string.Empty;
        PickUpDateTime = DateTime.Now.AddDays(1);
        Address = string.Empty;
    }

    private void AddProductToOrder(object rowData)
    {
        Product product = rowData as Product;
        OrderLine newOrderLine = new(product, 1, product.Price, 0);
        ResetLineNumber();
        newOrderLine.LineNumber = OrderLines.Count() + 1;
        OrderLines?.Add(newOrderLine);
        UpdateRunningTotal(); UpdateDiscountTotal();
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
        RunningTotal = (OrderLines?.Sum(ol => ol.Price * ol.Quantity));
    }

    private void UpdateDiscountTotal()
    {
        DiscountTotal = (OrderLines?.Sum(ol => ol.Discount * ol.Quantity));
    }

    private void IncreaseQuantity(object rowOrderLine)
    {
        (rowOrderLine as OrderLine).IncreaseQuantity();
        UpdateRunningTotal(); UpdateDiscountTotal();
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

    private void AddDiscountToOrderLine(object rowOrderLine)
    {
        (rowOrderLine as OrderLine).ApplyDiscount();
        UpdateRunningTotal(); UpdateDiscountTotal();
        OrderLinesCollectionView.Refresh();
    }

    private void AddPriceIncreaseToOrderLine(object rowOrderLine)
    {
        (rowOrderLine as OrderLine).IncreasePrice(PriceIncreaseAmount);
        UpdateRunningTotal();
        OrderLinesCollectionView.Refresh();
    }

    private void AddPaymentToOrder()
    {
        Payment newPayment = new(PaymentAmount, DateTime.Now, SelectedPaymentMethod);
        Payments.Add(newPayment);
        UpdateOutstandingAmount();

        SelectedPaymentMethod = null;
        PaymentAmount = decimal.Zero;
    }

    private void UpdateOutstandingAmount()
    {
        decimal totalPaid = Payments.Sum(p => p.Amount);
        OutstandingAmount = (RunningTotal ?? 0) - totalPaid;
    }

    private IPickUp HandleDelivery()
    {
        IPickUp delivery;
        IPickUp pickUp;
        if (IsDelivery) 
        {
            delivery = new Delivery (PickUpDateTime, Address);
            return delivery;
        }
        else
        {
            pickUp = new PickUp (PickUpDateTime);
            return pickUp;
        }
    }

}
