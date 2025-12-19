using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
namespace OrderManagerDesktopUI.ViewModels;

public class OrdersViewModel : ViewModelBase
{

    private readonly IOrderService _orderService;
    private ObservableCollection<Order> _orders { get; }
    public ICollectionView OrdersCollectionView { get; }

    private Order _selectedOrder;
    public Order SelectedOrder
    {
        get { return _selectedOrder; }
        set { _selectedOrder = value; OnPropertyChanged(); }
    }

    private string searchTerm = string.Empty;
    public string SearchTerm
    {
        get { return searchTerm; }
        set
        {
            searchTerm = value;
            OnPropertyChanged(nameof(OrderFilter));
            OrdersCollectionView.Refresh();
        }
    }

    public OrdersViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        _orders = new ObservableCollection<Order>(_orderService.GetAllOrders());
        OrdersCollectionView = CollectionViewSource.GetDefaultView(_orders);
        OrdersCollectionView.Filter = OrderFilter;
    }

    private bool OrderFilter(object obj)
    {
        if (obj is Order order)
        {
            return order.Id.ToString().Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                   order.Date.ToString().Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                   order.Customer.FirstName.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                   order.Customer.LastName.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase) || 
                   order.Note.Content.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
        }
        return false;
    }
}