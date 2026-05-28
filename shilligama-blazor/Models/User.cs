namespace shilligama_blazor.Models;

public class User
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // "cliente" | "trabajador" | "administrador"
}
