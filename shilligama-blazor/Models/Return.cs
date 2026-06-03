using System;
using System.Collections.Generic;

namespace shilligama_blazor.Models;

/// <summary>Representa un producto individual dentro de una devolución agrupada por pedido.</summary>
public class ReturnItem
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal => UnitPrice * Quantity;
}

public class Return
{
    public string Id { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;

    // ID del pedido al que pertenece la devolución
    public string OrderId { get; set; } = string.Empty;
    // Cliente del pedido
    public string Customer { get; set; } = string.Empty;

    // Campos legacy para compatibilidad con el panel de administración
    public string Product { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public int Quantity { get; set; }

    public string Reason { get; set; } = "Otro"; // "Vencido" | "Dañado" | "Error de pedido" | "Otro"
    public string RegisteredBy { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Observations { get; set; } = string.Empty;

    // Lista detallada de productos devueltos (agrupada por pedido)
    public List<ReturnItem> ReturnedProducts { get; set; } = new();
}
