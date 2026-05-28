using System;
using System.Collections.Generic;
using System.Linq;
using shilligama_blazor.Models;

namespace shilligama_blazor.Services;

public class StaffService
{
    private readonly List<Staff> _staff;

    public StaffService()
    {
        _staff = new List<Staff>
        {
            new()
            {
                Id = "STF-001",
                Nombre = "Admin Shiligama",
                Dni = "12345678",
                Rol = "administrador",
                Telefono = "951236487",
                Correo = "test1234@admin.com",
                Avatar = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=100&h=100&fit=crop",
                FechaIngreso = DateTime.Now.AddYears(-2),
                Estado = "activo"
            },
            new()
            {
                Id = "STF-002",
                Nombre = "Jorge Trabajador",
                Dni = "78965412",
                Rol = "trabajador",
                Telefono = "987654321",
                Correo = "trabajador@shiligama.com",
                Avatar = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=100&h=100&fit=crop",
                FechaIngreso = DateTime.Now.AddMonths(-6),
                Estado = "activo"
            },
            new()
            {
                Id = "STF-003",
                Nombre = "Marcos Díaz",
                Dni = "45632178",
                Rol = "trabajador",
                Telefono = "963852741",
                Correo = "marcos@shiligama.com",
                Avatar = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=100&h=100&fit=crop",
                FechaIngreso = DateTime.Now.AddMonths(-1),
                Estado = "activo"
            },
            new()
            {
                Id = "STF-004",
                Nombre = "Sofía Ramos",
                Dni = "95148726",
                Rol = "trabajador",
                Telefono = "954781236",
                Correo = "sofia@shiligama.com",
                Avatar = "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=100&h=100&fit=crop",
                FechaIngreso = DateTime.Now.AddMonths(-8),
                Estado = "inactivo"
            }
        };
    }

    public List<Staff> GetStaff() => _staff;

    public Staff? GetStaffById(string id) => _staff.FirstOrDefault(s => s.Id == id);

    public void AddStaff(Staff member)
    {
        var count = _staff.Count + 1;
        member.Id = $"STF-{count:D3}";
        member.FechaIngreso = DateTime.Now;
        if (string.IsNullOrWhiteSpace(member.Avatar))
        {
            member.Avatar = "https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?w=100&h=100&fit=crop";
        }
        _staff.Insert(0, member);
    }

    public void UpdateStaff(Staff member)
    {
        var existing = GetStaffById(member.Id);
        if (existing != null)
        {
            existing.Nombre = member.Nombre;
            existing.Dni = member.Dni;
            existing.Rol = member.Rol;
            existing.Telefono = member.Telefono;
            existing.Correo = member.Correo;
            existing.Avatar = member.Avatar;
            existing.Estado = member.Estado;
        }
    }

    public void DeleteStaff(string id)
    {
        var existing = GetStaffById(id);
        if (existing != null)
        {
            _staff.Remove(existing);
        }
    }

    public void ToggleStatus(string id)
    {
        var existing = GetStaffById(id);
        if (existing != null)
        {
            existing.Estado = existing.Estado == "activo" ? "inactivo" : "activo";
        }
    }
}
