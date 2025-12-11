using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
using OrderManagerLibrary.Services;
using System.Collections.ObjectModel;

namespace OrderManagerDesktopUI.ViewModels;
public class DashboardViewModel : ViewModel
{
    private readonly IOrderService _orderService;
    public ObservableCollection<Order> UpcomingOrders { get; private set; } = [];
    public ObservableCollection<Order> PendingPaymentOrders { get; private set; } = [];

    public DashboardViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        UpcomingOrders = new ObservableCollection<Order>(_orderService.GetUpcomingOrders());
        PendingPaymentOrders = new ObservableCollection<Order>(_orderService.GetPendingPaymentOrders());
    }
}
