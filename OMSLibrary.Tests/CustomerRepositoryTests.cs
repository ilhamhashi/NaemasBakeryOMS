using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class CustomerRepositoryTests
{
    private IRepository<Customer> _customerRepository;
    private IConfiguration _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _customerRepository = new CustomerRepository(_config);
    }

    [TestMethod]
    public void InsertCustomer_ShouldInsertCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer
        (
            1,
            "John",
            "Doe",
            "12345678"
        );

        // Act
        customer.CustomerId = _customerRepository.Insert(customer);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customer.CustomerId);
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        Assert.AreEqual(customer.LastName, retrievedCustomer.LastName);
        Assert.AreEqual(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }

    [TestMethod]
    public void UpdateCustomer_ShouldUpdateCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer
        (
            1,
            "John",
            "Doe",
            "12345678"
        );

        // Act
        customer.CustomerId = _customerRepository.Insert(customer);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customer.CustomerId);
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        Assert.AreEqual(customer.LastName, retrievedCustomer.LastName);
        Assert.AreEqual(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }

    [TestMethod]
    public void DeleteCustomer_ShouldDeleteCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer
        (
            1,
            "John",
            "Doe",
            "12345678"
        );

        // Act
        customer.CustomerId = _customerRepository.Insert(customer);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customer.CustomerId);
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        Assert.AreEqual(customer.LastName, retrievedCustomer.LastName);
        Assert.AreEqual(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }

    [TestMethod]
    public void GetById_ShouldGetCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer
        (
            1,
            "John",
            "Doe",
            "12345678"
        );

        // Act
        customer.CustomerId = _customerRepository.Insert(customer);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customer.CustomerId);
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        Assert.AreEqual(customer.LastName, retrievedCustomer.LastName);
        Assert.AreEqual(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }
}
