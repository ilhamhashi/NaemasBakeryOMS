using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class OrderRepositoryTests
{
    private IRepository<Order> _orderRepository;
    private IConfiguration _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _orderRepository = new OrderRepository(_config);
    }

    [TestMethod]
    public void InsertOrder_ShouldInsertOrderSuccesfully()
    {
        // Arrange
        var order = new Order
        (
            DateTime.Now,
            OrderStatus.Confirmed,
            1
        );

        // Act
        order.OrderId = _orderRepository.Insert(order);

        // Assert
        var retrievedOrder = _orderRepository.GetById(order.OrderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(order.OrderDate, retrievedOrder.OrderDate);
        Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
        Assert.AreEqual(order.Status, retrievedOrder.Status);
    }

    //[TestMethod]
    //public void UpdateOrder_ShouldUpdateOrderSuccesfully()
    //{
    //    // Arrange
    //    var order = new Order
    //    (
    //        DateTime.Now,
    //        OrderStatus.Confirmed,
    //        1
    //    );

    //    // Act
    //    order.OrderId = _orderRepository.Insert(order);

    //    // Assert
    //    var retrievedOrder = _orderRepository.GetById(order.OrderId);
    //    Assert.IsNotNull(retrievedOrder);
    //    Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
    //    Assert.AreEqual(order.Status, retrievedOrder.Status);
    //}

    //[TestMethod]
    //public void DeleteOrder_ShouldDeleteOrderSuccesfully()
    //{
    //    // Arrange
    //    var order = new Order
    //    (
    //        DateTime.Now,
    //        OrderStatus.Confirmed,
    //        1
    //    );

    //    // Act
    //    order.OrderId = _orderRepository.Insert(order);

    //    // Assert
    //    var retrievedOrder = _orderRepository.GetById(order.OrderId);
    //    Assert.IsNotNull(retrievedOrder);
    //    Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
    //    Assert.AreEqual(order.Status, retrievedOrder.Status);
    //}

    //[TestMethod]
    //public void GetById_ShouldGetOrderByIdSuccesfully()
    //{
    //    // Arrange
    //    var order = new Order
    //    (
    //        DateTime.Now,
    //        OrderStatus.Confirmed,
    //        1
    //    );

    //    // Act
    //    order.OrderId = _orderRepository.Insert(order);

    //    // Assert
    //    var retrievedOrder = _orderRepository.GetById(order.OrderId);
    //    Assert.IsNotNull(retrievedOrder);
    //    Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
    //    Assert.AreEqual(order.Status, retrievedOrder.Status);
    //}
}
