using System;
using shilligama_blazor.Models;

namespace shilligama_blazor.Services;

public class AuthService
{
    public User? CurrentUser { get; private set; }

    public event Action? OnChange;

    public bool Login(string email, string password)
    {
        // Administrador
        if (email == "test1234@admin.com" && password == "eldelegadolavandoselasmanos")
        {
            CurrentUser = new User { Email = email, Name = "Admin Shiligama", Role = "administrador" };
            NotifyStateChanged();
            return true;
        }
        
        // Trabajador
        if ((email == "trabajador@shiligama.com" || email == "carlos.mendoza@shiligama.com") && password == "trabajador123")
        {
            CurrentUser = new User { Email = "carlos.mendoza@shiligama.com", Name = "Carlos Mendoza", Role = "trabajador" };
            NotifyStateChanged();
            return true;
        }
        
        // Cliente
        if (email == "cliente@shiligama.com" && password == "cliente123")
        {
            CurrentUser = new User { Email = email, Name = "Juan Cliente", Role = "cliente" };
            NotifyStateChanged();
            return true;
        }

        // Cliente genérico si es un correo válido y tiene contraseña válida
        if (email.Contains("@") && password.Length >= 6)
        {
            CurrentUser = new User { Email = email, Name = email.Split('@')[0], Role = "cliente" };
            NotifyStateChanged();
            return true;
        }

        return false;
    }

    public void Register(string name, string email, string password)
    {
        // Simular registro y login automático del cliente
        CurrentUser = new User { Email = email, Name = name, Role = "cliente" };
        NotifyStateChanged();
    }

    public void Logout()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    public bool IsLoggedIn() => CurrentUser != null;
    
    public bool IsAdmin() => CurrentUser?.Role == "administrador";
    
    public bool IsWorker() => CurrentUser?.Role == "trabajador";

    private void NotifyStateChanged() => OnChange?.Invoke();
}
