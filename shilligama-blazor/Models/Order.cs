using System;

namespace shilligama_blazor.Models;

public class Order
{
    public string Id { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public int Items { get; set; }
    public string Status { get; set; } = "pendiente"; // "pendiente" | "preparando" | "en_camino" | "entregado" | "cancelado"
    public string Channel { get; set; } = "Online"; // "Online" | "Presencial"
}
