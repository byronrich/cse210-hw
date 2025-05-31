using System;
using System.Collections.Generic;

class Address
{
    private string _street;
    private string _city;
    private string _state;
    private string _country;

    public Address(string street, string city, string state, string country)
    {
        _street = street;
        _city = city;
        _state = state;
        _country = country;
    }

    public bool IsUSA() => _country.ToLower() == "usa";

    public string GetFullAddress() => $"{_street}\n{_city}, {_state}\n{_country}";
}

class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public bool LivesInUSA() => _address.IsUSA();

    public string GetShippingLabel() => $"{_name}\n{_address.GetFullAddress()}";
}

class Product
{
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public double GetTotalCost() => _price * _quantity;

    public string GetPackingLabel() => $"{_name} (ID: {_productId})";
}

class Order
{
    private List<Product> _products = new();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = 0;
        _products.ForEach(p => total += p.GetTotalCost());
        return total + (_customer.LivesInUSA() ? 5 : 35);
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        _products.ForEach(p => label += $"- {p.GetPackingLabel()}\n");
        return label;
    }

    public string GetShippingLabel() => $"Shipping Label:\n{_customer.GetShippingLabel()}";
}

class Program
{
    static void Main()
    {
        var order1 = new Order(new Customer("Alice Johnson", new Address("123 Elm St", "Springfield", "IL", "USA")));
        order1.AddProduct(new Product("Laptop", "A001", 999.99, 1));
        order1.AddProduct(new Product("Mouse", "B002", 25.50, 2));

        var order2 = new Order(new Customer("Bob Smith", new Address("45 Maple Ave", "Toronto", "ON", "Canada")));
        order2.AddProduct(new Product("Tablet", "C003", 499.99, 1));
        order2.AddProduct(new Product("Keyboard", "D004", 45.75, 1));

        var orders = new List<Order> { order1, order2 };

        orders.ForEach(order =>
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order.GetTotalCost():F2}\n");
        });
    }
}
