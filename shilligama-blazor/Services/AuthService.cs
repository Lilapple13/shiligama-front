using System.Text.Json;
using Microsoft.JSInterop;
using shilligama_blazor.Models;

namespace shilligama_blazor.Services;

public class AuthService
{
    private const string StorageKey = "shiligama-auth";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly IJSRuntime _jsRuntime;
    private bool _isInitialized;

    public User? CurrentUser { get; private set; }

    public event Action? OnChange;

    public AuthService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        try
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorageHelper.getItem", StorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                CurrentUser = JsonSerializer.Deserialize<User>(json, JsonOptions);
                NotifyStateChanged();
            }
        }
        catch
        {
            // Prerender / circuit not ready
        }

        _isInitialized = true;
    }

    public bool Login(string email, string password, bool rememberMe = false)
    {
        User? user = null;

        if (email == "test1234@admin.com" && password == "eldelegadolavandoselasmanos")
        {
            user = new User { Email = email, Name = "Admin Shiligama", Role = "administrador" };
        }
        else if ((email == "trabajador@shiligama.com" || email == "carlos.mendoza@shiligama.com") && password == "trabajador123")
        {
            user = new User { Email = "carlos.mendoza@shiligama.com", Name = "Carlos Mendoza", Role = "trabajador" };
        }
        else if (email == "cliente@shiligama.com" && password == "cliente123")
        {
            user = new User { Email = email, Name = "Juan Cliente", Role = "cliente" };
        }
        else if (email.Contains('@') && password.Length >= 6)
        {
            user = new User { Email = email, Name = email.Split('@')[0], Role = "cliente" };
        }

        if (user is null) return false;

        CurrentUser = user;
        _ = rememberMe ? PersistSessionAsync() : ClearPersistedSessionAsync();
        NotifyStateChanged();
        return true;
    }

    public void Register(string name, string email, string password)
    {
        CurrentUser = new User { Email = email, Name = name, Role = "cliente" };
        _ = PersistSessionAsync();
        NotifyStateChanged();
    }

    public void LoginAsGuest()
    {
        CurrentUser = new User { Email = "invitado@shiligama.com", Name = "Invitado", Role = "cliente" };
        _ = PersistSessionAsync();
        NotifyStateChanged();
    }

    public void Logout()
    {
        CurrentUser = null;
        _ = ClearPersistedSessionAsync();
        NotifyStateChanged();
    }

    public bool IsLoggedIn() => CurrentUser != null;

    public bool IsCliente() => CurrentUser?.Role == "cliente";

    public bool IsAdmin() => CurrentUser?.Role == "administrador";

    public bool IsWorker() => CurrentUser?.Role == "trabajador";

    private async Task PersistSessionAsync()
    {
        if (CurrentUser is null) return;

        try
        {
            var json = JsonSerializer.Serialize(CurrentUser, JsonOptions);
            await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", StorageKey, json);
        }
        catch
        {
            // Ignore during prerender
        }
    }

    private async Task ClearPersistedSessionAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorageHelper.removeItem", StorageKey);
        }
        catch
        {
            // Ignore during prerender
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
