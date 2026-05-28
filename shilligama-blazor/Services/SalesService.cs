using System;
using System.Collections.Generic;
using System.Linq;
using shilligama_blazor.Models;

namespace shilligama_blazor.Services;

public class SalesService
{
    private readonly List<Sale> _sales;
    private readonly List<Order> _orders;

    public SalesService()
    {
        // Seed recent customer orders
        _orders = new List<Order>
        {
            new() { Id = "ORD-001", Customer = "María García", Date = DateTime.Now.AddHours(-9), Total = 125.50m, Items = 8, Status = "entregado", Channel = "Online" },
            new() { Id = "ORD-002", Customer = "Carlos Mendoza", Date = DateTime.Now.AddHours(-10), Total = 89.00m, Items = 5, Status = "en_camino", Channel = "Online" },
            new() { Id = "ORD-003", Customer = "Ana Rodríguez", Date = DateTime.Now.AddHours(-11), Total = 234.80m, Items = 12, Status = "preparando", Channel = "Presencial" },
            new() { Id = "ORD-004", Customer = "Luis Torres", Date = DateTime.Now.AddHours(-12), Total = 56.00m, Items = 3, Status = "pendiente", Channel = "Online" },
            new() { Id = "ORD-005", Customer = "Rosa Sánchez", Date = DateTime.Now.AddHours(-13), Total = 178.90m, Items = 9, Status = "entregado", Channel = "Presencial" },
            new() { Id = "ORD-006", Customer = "Pedro Díaz", Date = DateTime.Now.AddDays(-1), Total = 312.40m, Items = 15, Status = "cancelado", Channel = "Online" }
        };

        // Seed administrative sales history
        _sales = new List<Sale>
        {
            new() { Id = "VTA-001", Fecha = DateTime.Now.AddHours(-2), Cliente = "María García", Canal = "web", Total = 125.50m, MetodoPago = "tarjeta", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-002", Fecha = DateTime.Now.AddHours(-4), Cliente = "Carlos Mendoza", Canal = "web", Total = 89.00m, MetodoPago = "yape", Comprobante = "ninguno", Estado = "pendiente" },
            new() { Id = "VTA-003", Fecha = DateTime.Now.AddHours(-6), Cliente = "Público General", Canal = "presencial", Total = 45.20m, MetodoPago = "efectivo", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-004", Fecha = DateTime.Now.AddHours(-8), Cliente = "Ana Rodríguez", Canal = "presencial", Total = 234.80m, MetodoPago = "tarjeta", Comprobante = "factura", Estado = "completado" },
            new() { Id = "VTA-005", Fecha = DateTime.Now.AddDays(-1), Cliente = "Luis Torres", Canal = "web", Total = 56.00m, MetodoPago = "plin", Comprobante = "ninguno", Estado = "completado" },
            new() { Id = "VTA-006", Fecha = DateTime.Now.AddDays(-1), Cliente = "Rosa Sánchez", Canal = "presencial", Total = 178.90m, MetodoPago = "efectivo", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-007", Fecha = DateTime.Now.AddDays(-2), Cliente = "Pedro Díaz", Canal = "web", Total = 312.40m, MetodoPago = "tarjeta", Comprobante = "factura", Estado = "completado" },
            new() { Id = "VTA-008", Fecha = DateTime.Now.AddDays(-2), Cliente = "Público General", Canal = "presencial", Total = 12.50m, MetodoPago = "yape", Comprobante = "ninguno", Estado = "completado" },
            new() { Id = "VTA-009", Fecha = DateTime.Now.AddDays(-3), Cliente = "Jorge Valdivia", Canal = "whatsapp", Total = 95.00m, MetodoPago = "yape", Comprobante = "boleta", Estado = "completado" }
        };
    }

    public List<Order> GetRecentOrders() => _orders;

    public List<Sale> GetSales() => _sales;

    public void AddOrder(string customerName, List<CartItem> items, decimal total, string channel)
    {
        var count = _orders.Count + 1;
        var orderId = $"ORD-{count:D3}";
        _orders.Insert(0, new Order
        {
            Id = orderId,
            Customer = customerName,
            Date = DateTime.Now,
            Total = total,
            Items = items.Sum(i => i.Quantity),
            Status = "pendiente",
            Channel = channel
        });

        // Also record as a Sale
        var saleId = $"VTA-{_sales.Count + 1:D3}";
        _sales.Insert(0, new Sale
        {
            Id = saleId,
            Fecha = DateTime.Now,
            Cliente = customerName,
            Canal = channel.ToLower() == "online" ? "web" : "presencial",
            Productos = items,
            Total = total,
            MetodoPago = "tarjeta",
            Comprobante = "boleta",
            Estado = "pendiente"
        });
    }

    public void AddPresencialSale(string customerName, List<CartItem> items, decimal total, string payMethod, string invoiceType)
    {
        var saleId = $"VTA-{_sales.Count + 1:D3}";
        _sales.Insert(0, new Sale
        {
            Id = saleId,
            Fecha = DateTime.Now,
            Cliente = string.IsNullOrWhiteSpace(customerName) ? "Público General" : customerName,
            Canal = "presencial",
            Productos = items,
            Total = total,
            MetodoPago = payMethod,
            Comprobante = invoiceType,
            Estado = "completado"
        });

        // Also add to orders for dashboard display
        var orderId = $"ORD-{_orders.Count + 1:D3}";
        _orders.Insert(0, new Order
        {
            Id = orderId,
            Customer = string.IsNullOrWhiteSpace(customerName) ? "Público General" : customerName,
            Date = DateTime.Now,
            Total = total,
            Items = items.Sum(i => i.Quantity),
            Status = "entregado",
            Channel = "Presencial"
        });
    }

    public void UpdateOrderStatus(string id, string status)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.Status = status;
        }

        // Sync with Sale if needed
        var saleId = id.Replace("ORD-", "VTA-");
        var sale = _sales.FirstOrDefault(s => s.Id == saleId);
        if (sale != null)
        {
            sale.Estado = status == "entregado" ? "completado" : status;
        }
    }

    public void DeleteOrder(string id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null) _orders.Remove(order);
    }
}
