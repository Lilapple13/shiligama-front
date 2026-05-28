using System;
using System.Collections.Generic;
using System.Linq;
using shilligama_blazor.Models;

namespace shilligama_blazor.Services;

public class ReturnsService
{
    private readonly List<Return> _returns;

    public ReturnsService()
    {
        _returns = new List<Return>
        {
            new()
            {
                Id = "DEV-001",
                Date = DateTime.Now.AddDays(-2),
                Product = "Pechuga de Pollo 1kg",
                ProductCode = "PROD-009",
                Quantity = 2,
                Reason = "Vencido",
                RegisteredBy = "Jorge Trabajador",
                Amount = 33.80m,
                Observations = "El empaque presentaba fecha vencida de hace 2 días."
            },
            new()
            {
                Id = "DEV-002",
                Date = DateTime.Now.AddDays(-5),
                Product = "Aceite Primor 1L",
                ProductCode = "PROD-002",
                Quantity = 1,
                Reason = "Dañado",
                RegisteredBy = "Marcos Díaz",
                Amount = 12.50m,
                Observations = "Envase abollado con pérdida de contenido en almacén."
            },
            new()
            {
                Id = "DEV-003",
                Date = DateTime.Now.AddDays(-10),
                Product = "Inca Kola 1.5L",
                ProductCode = "PROD-003",
                Quantity = 3,
                Reason = "Error de pedido",
                RegisteredBy = "Admin Shiligama",
                Amount = 19.50m,
                Observations = "Se entregó Coca Cola en lugar de Inca Kola."
            }
        };
    }

    public List<Return> GetReturns() => _returns;

    public Return? GetReturnById(string id) => _returns.FirstOrDefault(r => r.Id == id);

    public void AddReturn(Return returnItem)
    {
        var count = _returns.Count + 1;
        returnItem.Id = $"DEV-{count:D3}";
        returnItem.Date = DateTime.Now;
        _returns.Insert(0, returnItem);
    }

    public void UpdateReturn(Return returnItem)
    {
        var existing = GetReturnById(returnItem.Id);
        if (existing != null)
        {
            existing.Product = returnItem.Product;
            existing.ProductCode = returnItem.ProductCode;
            existing.Quantity = returnItem.Quantity;
            existing.Reason = returnItem.Reason;
            existing.RegisteredBy = returnItem.RegisteredBy;
            existing.Amount = returnItem.Amount;
            existing.Observations = returnItem.Observations;
        }
    }

    public void DeleteReturn(string id)
    {
        var existing = GetReturnById(id);
        if (existing != null)
        {
            _returns.Remove(existing);
        }
    }
}
