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
            new() {
                Id = "SHI-2024-0156", Customer = "Juan Cliente",
                Date = DateTime.Now.AddHours(-1.5),
                Subtotal = 102.50m, DeliveryFee = 5.00m, Total = 107.50m,
                Items = 4, Status = "entregado", DeliveryMethod = "delivery",
                PaymentMethod = "yape", Address = "Av. Los Álamos 123, San Isidro",
                Products = new() {
                    new() { Id=1, Name="Arroz Costeño 5kg",   Price=24.90m, Quantity=2 },
                    new() { Id=2, Name="Aceite Primor 1L",     Price=12.50m, Quantity=1 },
                    new() { Id=3, Name="Leche Gloria 1L",      Price=5.20m,  Quantity=6 },
                    new() { Id=4, Name="Azúcar Rubia 1kg",     Price=4.80m,  Quantity=2 },
                },
                TimelinePedidoRecibido  = DateTime.Now.AddHours(-1.5),
                TimelineEnPreparacion   = DateTime.Now.AddHours(-1.3),
                TimelineListo           = DateTime.Now.AddHours(-1.0),
                TimelineEnCamino        = DateTime.Now.AddMinutes(-45),
                TimelineEntregado       = DateTime.Now.AddMinutes(-15),
            },
            new() {
                Id = "SHI-2024-0155", Customer = "María Torres",
                Date = DateTime.Now.AddHours(-3),
                Subtotal = 40.40m, DeliveryFee = 5.00m, Total = 45.40m,
                Items = 3, Status = "en_camino", DeliveryMethod = "delivery",
                PaymentMethod = "plin", Address = "Jr. Las Flores 456, Miraflores",
                Products = new() {
                    new() { Id=1, Name="Gaseosa Inca Kola 3L", Price=12.00m, Quantity=2 },
                    new() { Id=2, Name="Galletas Oreo 6-pack", Price=8.50m,  Quantity=1 },
                    new() { Id=3, Name="Pan de Molde Bimbo",   Price=7.90m,  Quantity=1 },
                },
                TimelinePedidoRecibido  = DateTime.Now.AddHours(-3),
                TimelineEnPreparacion   = DateTime.Now.AddHours(-2.7),
                TimelineListo           = DateTime.Now.AddHours(-2),
                TimelineEnCamino        = DateTime.Now.AddMinutes(-75),
            },
            new() {
                Id = "SHI-2024-0150", Customer = "Carlos Mendoza",
                Date = DateTime.Now.AddHours(-5),
                Subtotal = 47.90m, DeliveryFee = 0m, Total = 47.90m,
                Items = 2, Status = "cancelado", DeliveryMethod = "pickup",
                PaymentMethod = "efectivo", Address = "",
                Products = new() {
                    new() { Id=1, Name="Detergente Bolivar 2kg", Price=28.90m, Quantity=1 },
                    new() { Id=2, Name="Jabón Bolívar Pack x3",  Price=9.50m,  Quantity=2 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddHours(-5),
                TimelineCancelado      = DateTime.Now.AddHours(-4.7),
            },

            new() {
                Id = "SHI-2024-0148", Customer = "Ana Rodríguez",
                Date = DateTime.Now.AddHours(-4),
                Subtotal = 68.00m, DeliveryFee = 0m, Total = 68.00m,
                Items = 5, Status = "entregado", DeliveryMethod = "pickup",
                PaymentMethod = "tarjeta", Address = "",
                Products = new() {
                    new() { Id=1, Name="Yogurt Gloria 1L",        Price=8.90m,  Quantity=3 },
                    new() { Id=2, Name="Queso Fresco 500g",        Price=15.00m, Quantity=1 },
                    new() { Id=3, Name="Jamón del Norte 200g",     Price=12.50m, Quantity=1 },
                    new() { Id=4, Name="Pan Francés x10",          Price=3.00m,  Quantity=2 },
                    new() { Id=5, Name="Mantequilla Gloria 200g",  Price=7.80m,  Quantity=1 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddHours(-4),
                TimelineEnPreparacion  = DateTime.Now.AddHours(-3.7),
                TimelineListo          = DateTime.Now.AddHours(-3),
                TimelineEntregado      = DateTime.Now.AddHours(-2.5),
            },
            new() {
                Id = "SHI-2024-0145", Customer = "Luis Peña",
                Date = DateTime.Now.AddMinutes(-45),
                Subtotal = 52.00m, DeliveryFee = 5.00m, Total = 57.00m,
                Items = 4, Status = "preparando", DeliveryMethod = "delivery",
                PaymentMethod = "yape", Address = "Av. Los Álamos 123, San Isidro",
                Products = new() {
                    new() { Id=1, Name="Pollo Entero 2.5kg", Price=32.50m, Quantity=1 },
                    new() { Id=2, Name="Papa Blanca 3kg",    Price=9.00m,  Quantity=1 },
                    new() { Id=3, Name="Cebolla Roja 1kg",   Price=4.50m,  Quantity=1 },
                    new() { Id=4, Name="Ají Amarillo 250g",  Price=3.00m,  Quantity=2 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddMinutes(-45),
                TimelineEnPreparacion  = DateTime.Now.AddMinutes(-30),
            },
            new() {
                Id = "SHI-2024-0140", Customer = "Rosa Sánchez",
                Date = DateTime.Now.AddMinutes(-20),
                Subtotal = 35.50m, DeliveryFee = 5.00m, Total = 40.50m,
                Items = 3, Status = "pendiente", DeliveryMethod = "delivery",
                PaymentMethod = "plin", Address = "Jr. Las Flores 456, Miraflores",
                Products = new() {
                    new() { Id=1, Name="Fideos Don Vittorio 500g", Price=4.50m,  Quantity=4 },
                    new() { Id=2, Name="Salsa de Tomate 200g",     Price=3.50m,  Quantity=2 },
                    new() { Id=3, Name="Atún Florida 170g x3",     Price=14.50m, Quantity=1 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddMinutes(-20),
            },
            new() {
                Id = "SHI-2024-0138", Customer = "Pedro Díaz",
                Date = DateTime.Now.AddHours(-6),
                Subtotal = 88.00m, DeliveryFee = 5.00m, Total = 93.00m,
                Items = 3, Status = "entregado", DeliveryMethod = "delivery",
                PaymentMethod = "tarjeta", Address = "Jr. Cusco 789, Barranco",
                Products = new() {
                    new() { Id=1, Name="Leche Gloria 1L", Price=5.20m, Quantity=6 },
                    new() { Id=2, Name="Yogurt Gloria 1L", Price=8.90m, Quantity=3 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddHours(-6),
                TimelineEnPreparacion  = DateTime.Now.AddHours(-5.5),
                TimelineListo          = DateTime.Now.AddHours(-5),
                TimelineEnCamino       = DateTime.Now.AddHours(-4),
                TimelineEntregado      = DateTime.Now.AddHours(-3),
            },
            new() {
                Id = "SHI-2024-0135", Customer = "Carla Mendoza",
                Date = DateTime.Now.AddHours(-8),
                Subtotal = 52.50m, DeliveryFee = 0m, Total = 52.50m,
                Items = 2, Status = "entregado", DeliveryMethod = "pickup",
                PaymentMethod = "efectivo", Address = "",
                Products = new() {
                    new() { Id=1, Name="Detergente Ariel 2kg", Price=32.50m, Quantity=1 },
                    new() { Id=2, Name="Jabón Bolívar Pack x3", Price=9.50m, Quantity=2 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddHours(-8),
                TimelineEntregado      = DateTime.Now.AddHours(-7),
            },
            new() {
                Id = "SHI-2024-0130", Customer = "Gustavo Ríos",
                Date = DateTime.Now.AddMinutes(-10),
                Subtotal = 76.00m, DeliveryFee = 5.00m, Total = 81.00m,
                Items = 4, Status = "pendiente", DeliveryMethod = "delivery",
                PaymentMethod = "plin", Address = "Av. Brasil 321, Pueblo Libre",
                Products = new() {
                    new() { Id=1, Name="Arroz Costeño 5kg", Price=24.90m, Quantity=2 },
                    new() { Id=2, Name="Aceite Primor 1L", Price=12.50m, Quantity=2 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddMinutes(-10),
            },
            new() {
                Id = "SHI-2024-0128", Customer = "Miriam Castro",
                Date = DateTime.Now.AddMinutes(-35),
                Subtotal = 44.50m, DeliveryFee = 5.00m, Total = 49.50m,
                Items = 3, Status = "preparando", DeliveryMethod = "delivery",
                PaymentMethod = "yape", Address = "Av. Universitaria 654, SMP",
                Products = new() {
                    new() { Id=1, Name="Gaseosa Inca Kola 3L", Price=12.00m, Quantity=2 },
                    new() { Id=2, Name="Galletas Oreo 6-pack", Price=8.50m, Quantity=1 },
                    new() { Id=3, Name="Agua San Luis 2.5L", Price=3.75m, Quantity=3 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddMinutes(-35),
                TimelineEnPreparacion  = DateTime.Now.AddMinutes(-20),
            },
            new() {
                Id = "SHI-2024-0125", Customer = "Roberto Herrera",
                Date = DateTime.Now.AddHours(-7),
                Subtotal = 60.00m, DeliveryFee = 0m, Total = 60.00m,
                Items = 2, Status = "cancelado", DeliveryMethod = "pickup",
                PaymentMethod = "efectivo", Address = "",
                Products = new() {
                    new() { Id=1, Name="Pollo Entero 2.5kg", Price=32.50m, Quantity=1 },
                    new() { Id=2, Name="Papa Blanca 3kg", Price=9.00m, Quantity=3 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddHours(-7),
                TimelineCancelado      = DateTime.Now.AddHours(-6.5),
            },
            new() {
                Id = "SHI-2024-0122", Customer = "Valeria Torres",
                Date = DateTime.Now.AddHours(-2),
                Subtotal = 95.00m, DeliveryFee = 5.00m, Total = 100.00m,
                Items = 5, Status = "en_camino", DeliveryMethod = "delivery",
                PaymentMethod = "tarjeta", Address = "Calle Las Begonias 430, San Isidro",
                Products = new() {
                    new() { Id=1, Name="Queso Fresco 500g", Price=15.00m, Quantity=2 },
                    new() { Id=2, Name="Jamón del Norte 200g", Price=12.50m, Quantity=3 },
                },
                TimelinePedidoRecibido = DateTime.Now.AddHours(-2),
                TimelineEnPreparacion  = DateTime.Now.AddHours(-1.7),
                TimelineListo          = DateTime.Now.AddHours(-1.2),
                TimelineEnCamino       = DateTime.Now.AddMinutes(-45),
            },
        };

        // Seed administrative sales history
        _sales = new List<Sale>
        {
            new() { Id = "VTA-001", Fecha = DateTime.Now.AddHours(-2), Cliente = "María García", Canal = "web", Total = 125.50m, MetodoPago = "tarjeta", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-002", Fecha = DateTime.Now.AddHours(-4), Cliente = "Carlos Mendoza", Canal = "web", Total = 89.00m, MetodoPago = "yape", Comprobante = "ninguno", Estado = "completado" },
            new() { Id = "VTA-003", Fecha = DateTime.Now.AddHours(-6), Cliente = "Público General", Canal = "presencial", Total = 45.20m, MetodoPago = "efectivo", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-004", Fecha = DateTime.Now.AddHours(-8), Cliente = "Ana Rodríguez", Canal = "presencial", Total = 234.80m, MetodoPago = "tarjeta", Comprobante = "factura", Estado = "completado" },
            new() { Id = "VTA-005", Fecha = DateTime.Now.AddDays(-1), Cliente = "Luis Torres", Canal = "web", Total = 56.00m, MetodoPago = "plin", Comprobante = "ninguno", Estado = "completado" },
            new() { Id = "VTA-006", Fecha = DateTime.Now.AddDays(-1), Cliente = "Rosa Sánchez", Canal = "presencial", Total = 178.90m, MetodoPago = "efectivo", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-007", Fecha = DateTime.Now.AddDays(-2), Cliente = "Pedro Díaz", Canal = "web", Total = 312.40m, MetodoPago = "tarjeta", Comprobante = "factura", Estado = "completado" },
            new() { Id = "VTA-008", Fecha = DateTime.Now.AddDays(-2), Cliente = "Público General", Canal = "presencial", Total = 12.50m, MetodoPago = "yape", Comprobante = "ninguno", Estado = "completado" },
            new() { Id = "VTA-009", Fecha = DateTime.Now.AddDays(-3), Cliente = "Jorge Valdivia", Canal = "web", Total = 95.00m, MetodoPago = "yape", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-010", Fecha = DateTime.Now.AddDays(-4), Cliente = "Carmen Villanueva", Canal = "presencial", Total = 67.30m, MetodoPago = "efectivo", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-011", Fecha = DateTime.Now.AddDays(-5), Cliente = "Ricardo Huanca", Canal = "web", Total = 143.20m, MetodoPago = "tarjeta", Comprobante = "factura", Estado = "completado" },
            new() { Id = "VTA-012", Fecha = DateTime.Now.AddDays(-5), Cliente = "Lucía Paredes", Canal = "presencial", Total = 38.50m, MetodoPago = "yape", Comprobante = "ninguno", Estado = "completado" },
            new() { Id = "VTA-013", Fecha = DateTime.Now.AddDays(-6), Cliente = "Público General", Canal = "presencial", Total = 22.00m, MetodoPago = "efectivo", Comprobante = "ninguno", Estado = "completado" },
            new() { Id = "VTA-014", Fecha = DateTime.Now.AddDays(-7), Cliente = "Fabiola Quispe", Canal = "web", Total = 189.90m, MetodoPago = "tarjeta", Comprobante = "factura", Estado = "completado" },
            new() { Id = "VTA-015", Fecha = DateTime.Now.AddDays(-8), Cliente = "Ernesto Castillo", Canal = "presencial", Total = 55.40m, MetodoPago = "plin", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-016", Fecha = DateTime.Now.AddDays(-9), Cliente = "Norma Salazar", Canal = "web", Total = 210.00m, MetodoPago = "tarjeta", Comprobante = "factura", Estado = "completado" },
            new() { Id = "VTA-017", Fecha = DateTime.Now.AddDays(-10), Cliente = "Humberto Flores", Canal = "presencial", Total = 48.75m, MetodoPago = "efectivo", Comprobante = "boleta", Estado = "completado" },
            new() { Id = "VTA-018", Fecha = DateTime.Now.AddDays(-11), Cliente = "Patricia Cruz", Canal = "web", Total = 77.60m, MetodoPago = "yape", Comprobante = "ninguno", Estado = "completado" },
            new() { Id = "VTA-019", Fecha = DateTime.Now.AddDays(-12), Cliente = "Alejandro Mamani", Canal = "presencial", Total = 130.00m, MetodoPago = "tarjeta", Comprobante = "factura", Estado = "completado" },
            new() { Id = "VTA-020", Fecha = DateTime.Now.AddDays(-13), Cliente = "Gloria Espinoza", Canal = "web", Total = 63.20m, MetodoPago = "plin", Comprobante = "boleta", Estado = "completado" }
        };
    }

    public List<Order> GetRecentOrders() => _orders;

    public List<Sale> GetSales() => _sales;

    public void AddOrder(string customerName, List<CartItem> items, decimal subtotal,
                     decimal deliveryFee, decimal total,
                     string paymentMethod, string deliveryMethod, string address)
    {
        var count = _orders.Count + 1;
        var orderId = $"SHI-2024-{count:D4}";
        _orders.Insert(0, new Order
        {
            Id = orderId,
            Customer = customerName,
            Date = DateTime.Now,
            Subtotal = subtotal,
            DeliveryFee = deliveryFee,
            Total = total,
            Items = items.Sum(i => i.Quantity),
            Status = "pendiente",
            DeliveryMethod = deliveryMethod,
            PaymentMethod = paymentMethod,
            Address = address,
            Products = new List<CartItem>(items),
            TimelinePedidoRecibido = DateTime.Now,
        });

        var saleId = $"VTA-{_sales.Count + 1:D3}";
        _sales.Insert(0, new Sale
        {
            Id = saleId,
            Fecha = DateTime.Now,
            Cliente = customerName,
            Canal = "web",
            Productos = items,
            Total = total,
            MetodoPago = paymentMethod,
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
