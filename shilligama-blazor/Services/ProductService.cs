using System.Collections.Generic;
using System.Linq;
using shilligama_blazor.Models;

namespace shilligama_blazor.Services;

public class ProductService
{
    private readonly List<Product> _products;
    private readonly List<Category> _categories;

    public string SearchQuery { get; set; } = string.Empty;
    public event System.Action? OnSearch;

    public void NotifySearch(string query)
    {
        SearchQuery = query;
        OnSearch?.Invoke();
    }

    public ProductService()
    {
        _categories = new List<Category>
        {
            new() { Id = "abarrotes", Name = "Abarrotes" },
            new() { Id = "bebidas", Name = "Bebidas" },
            new() { Id = "lacteos", Name = "Lácteos" },
            new() { Id = "snacks", Name = "Snacks" },
            new() { Id = "limpieza", Name = "Limpieza" },
            new() { Id = "carnes", Name = "Carnes y Embutidos" },
            new() { Id = "frutas", Name = "Frutas y Verduras" },
            new() { Id = "dulces", Name = "Dulces y Golosinas" },
            new() { Id = "congelados", Name = "Congelados" },
            new() { Id = "bebes", Name = "Bebés" }
        };

        _products = new List<Product>
        {
            new() {
                Id = 1,
                Name = "Arroz Extra Costeño 5kg",
                Price = 24.90m,
                Image = "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300&h=300&fit=crop",
                Category = "abarrotes",
                Stock = 45,
                Description = "Arroz de grano largo de la más alta calidad. Ideal para todo tipo de preparaciones. Rinde más y queda suelto."
            },
            new() {
                Id = 2,
                Name = "Aceite Primor 1L",
                Price = 12.50m,
                OriginalPrice = 15.90m,
                Image = "https://images.unsplash.com/photo-1474979266404-7eaacbcd87c5?w=300&h=300&fit=crop",
                Category = "abarrotes",
                Stock = 32,
                IsPromo = true,
                Description = "Aceite vegetal premium para cocinar. Bajo en grasas saturadas. Ideal para freír, saltear y hornear."
            },
            new() {
                Id = 3,
                Name = "Inca Kola 1.5L",
                Price = 6.50m,
                Image = "https://images.unsplash.com/photo-1622483767028-3f66f32aef97?w=300&h=300&fit=crop",
                Category = "bebidas",
                Stock = 78,
                Description = "La bebida del sabor nacional. Refrescante y con ese sabor único que todos conocemos. Botella familiar de 1.5 litros."
            },
            new() {
                Id = 4,
                Name = "Leche Gloria Entera 1L",
                Price = 5.20m,
                Image = "https://images.unsplash.com/photo-1563636619-e9143da7973b?w=300&h=300&fit=crop",
                Category = "lacteos",
                Stock = 120,
                Description = "Leche entera evaporada. Rica en calcio y vitaminas. Perfecta para toda la familia."
            },
            new() {
                Id = 5,
                Name = "Yogurt Laive Fresa 1L",
                Price = 8.90m,
                OriginalPrice = 10.50m,
                Image = "https://images.unsplash.com/photo-1488477181946-6428a0291777?w=300&h=300&fit=crop",
                Category = "lacteos",
                Stock = 25,
                IsPromo = true,
                Description = "Yogurt cremoso sabor fresa con probióticos. Ideal para el desayuno o como snack saludable."
            },
            new() {
                Id = 6,
                Name = "Galletas Oreo x6",
                Price = 4.50m,
                Image = "https://images.unsplash.com/photo-1558961363-fa8fdf82db35?w=300&h=300&fit=crop",
                Category = "snacks",
                Stock = 65,
                Description = "Galletas de chocolate con relleno de crema de vainilla. El snack favorito de todos. Pack de 6 unidades."
            },
            new() {
                Id = 7,
                Name = "Detergente Bolívar 2.6kg",
                Price = 28.90m,
                Image = "https://images.unsplash.com/photo-1582735689369-4fe89db7114c?w=300&h=300&fit=crop",
                Category = "limpieza",
                Stock = 18,
                Description = "Detergente en polvo de alta eficiencia. Remueve las manchas más difíciles y deja la ropa con un aroma fresco."
            },
            new() {
                Id = 8,
                Name = "Jabón Bolívar Pack x3",
                Price = 9.90m,
                OriginalPrice = 12.90m,
                Image = "https://images.unsplash.com/photo-1600857544200-b2f666a9a2ec?w=300&h=300&fit=crop",
                Category = "limpieza",
                Stock = 42,
                IsPromo = true,
                MinStock = 15,
                Description = "Jabón de lavar ropa de alta calidad. Elimina manchas difíciles y cuida tus prendas. Pack económico de 3 barras."
            },
            new() {
                Id = 9,
                Name = "Pechuga de Pollo 1kg",
                Price = 16.90m,
                Image = "https://images.unsplash.com/photo-1604503468506-a8da13d82791?w=300&h=300&fit=crop",
                Category = "carnes",
                Stock = 8,
                MinStock = 12,
                Description = "Pechuga de pollo fresca sin hueso. Alta en proteínas y baja en grasa. Perfecta para dietas saludables."
            },
            new() {
                Id = 10,
                Name = "Manzana Roja 1kg",
                Price = 5.90m,
                Image = "https://images.unsplash.com/photo-1560806887-1e4cd0b6cbd6?w=300&h=300&fit=crop",
                Category = "frutas",
                Stock = 35,
                Description = "Manzanas rojas frescas y crujientes. Ricas en fibra y vitaminas. Ideales para consumir como snack."
            },
            new() {
                Id = 11,
                Name = "Plátano de Seda 1kg",
                Price = 3.50m,
                Image = "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?w=300&h=300&fit=crop",
                Category = "frutas",
                Stock = 50,
                Description = "Plátanos de seda maduros y dulces. Excelente fuente de potasio y energía natural."
            },
            new() {
                Id = 12,
                Name = "Chocolate Sublime x6",
                Price = 7.90m,
                Image = "https://images.unsplash.com/photo-1511381939415-e44015466834?w=300&h=300&fit=crop",
                Category = "dulces",
                Stock = 88,
                Description = "Chocolate con leche y maní. El clásico peruano que todos aman. Pack de 6 barras."
            },
            new() {
                Id = 13,
                Name = "Helado D'onofrio 1L",
                Price = 18.90m,
                OriginalPrice = 22.90m,
                Image = "https://images.unsplash.com/photo-1497034825429-c343d7c6a68f?w=300&h=300&fit=crop",
                Category = "congelados",
                Stock = 12,
                MinStock = 15, 
                IsPromo = true,
                Description = "Helado cremoso de vainilla y chocolate. El postre perfecto para compartir en familia. Envase de 1 litro."
            },
            new() {
                Id = 14,
                Name = "Pañales Huggies M x40",
                Price = 45.90m,
                Image = "https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?w=300&h=300&fit=crop",
                Category = "bebes",
                Stock = 15,
                MinStock = 20,
                Description = "Pañales ultra absorbentes talla M. Protección por hasta 12 horas. Suaves con la piel del bebé."
            },
            new() {
                Id = 15,
                Name = "Agua San Luis 2.5L",
                Price = 3.20m,
                Image = "https://images.unsplash.com/photo-1548839140-29a749e1cf4d?w=300&h=300&fit=crop",
                Category = "bebidas",
                Stock = 20,
                Description = "Agua mineral natural de manantial. Pureza garantizada. Hidratación para toda la familia."
            },
            new() {
                Id = 16,
                Name = "Papas Lays Original 150g",
                Price = 6.90m,
                Image = "https://images.unsplash.com/photo-1566478989037-eec170784d0b?w=300&h=300&fit=crop",
                Category = "snacks",
                Stock = 0,
                Description = "Papas fritas crujientes con sal. El snack clásico para cualquier ocasión. Bolsa de 150g."
            },
            new() {
                Id = 17,
                Name = "Gaseosa Coca-Cola 1.5L",
                Price = 6.80m,
                Image = "https://images.unsplash.com/photo-1622483767028-3f66f32aef97?w=300&h=300&fit=crop",
                Category = "bebidas",
                Stock = 50,
                Description = "Gaseosa refrescante Coca-Cola sabor original de 1.5 litros."
            },
            new() {
                Id = 18,
                Name = "Galletas Soda San Jorge Pack x6",
                Price = 3.80m,
                Image = "https://images.unsplash.com/photo-1558961363-fa8fdf82db35?w=300&h=300&fit=crop",
                Category = "snacks",
                Stock = 90,
                Description = "Galletas de soda crujientes ideales para acompañar con queso o mermelada."
            },
            new() {
                Id = 19,
                Name = "Arroz Costeño Integral 1kg",
                Price = 6.20m,
                Image = "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300&h=300&fit=crop",
                Category = "abarrotes",
                Stock = 25,
                Description = "Arroz integral premium de grano largo, alto en fibra y nutrientes."
            },
            new() {
                Id = 20,
                Name = "Margarina La Danesa 250g",
                Price = 4.90m,
                Image = "https://images.unsplash.com/photo-1563636619-e9143da7973b?w=300&h=300&fit=crop",
                Category = "lacteos",
                Stock = 40,
                Description = "Margarina con sal, ideal para untar y repostería."
            },
            new() {
                Id = 21,
                Name = "Detergente Opal Ultra 2kg",
                Price = 22.50m,
                OriginalPrice = 26.90m,
                Image = "https://images.unsplash.com/photo-1582735689369-4fe89db7114c?w=300&h=300&fit=crop",
                Category = "limpieza",
                Stock = 15,
                MinStock = 15,
                IsPromo = true,
                Description = "Detergente en polvo multiusos para el cuidado de prendas de color y blancas."
            },
            new() {
                Id = 22,
                Name = "Lomo Fino de Res 1kg",
                Price = 45.90m,
                Image = "https://images.unsplash.com/photo-1604503468506-a8da13d82791?w=300&h=300&fit=crop",
                Category = "carnes",
                Stock = 12,
                MinStock = 15,
                Description = "Corte de carne de res sumamente tierno y jugoso para parrilladas o lomo saltado."
            },
            new() {
                Id = 23,
                Name = "Naranja para Jugo 1kg",
                Price = 3.90m,
                Image = "https://images.unsplash.com/photo-1560806887-1e4cd0b6cbd6?w=300&h=300&fit=crop",
                Category = "frutas",
                Stock = 60,
                Description = "Naranjas frescas y jugosas, ideales para preparar jugos por las mañanas."
            },
            new() {
                Id = 24,
                Name = "Caramelos de Limón Limonada Pack x30",
                Price = 4.20m,
                Image = "https://images.unsplash.com/photo-1511381939415-e44015466834?w=300&h=300&fit=crop",
                Category = "dulces",
                Stock = 100,
                Description = "Caramelos duros sabor limón con relleno ácido."
            },
            new() {
                Id = 25,
                Name = "Papas Prefritas Congeladas 1kg",
                Price = 11.90m,
                Image = "https://images.unsplash.com/photo-1497034825429-c343d7c6a68f?w=300&h=300&fit=crop",
                Category = "congelados",
                Stock = 20,
                MinStock = 15,
                Description = "Papas cortadas estilo francés listas para freír u hornear."
            },
            new() {
                Id = 26,
                Name = "Papilla Nestum de Trigo y Miel 350g",
                Price = 14.50m,
                OriginalPrice = 17.20m,
                Image = "https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?w=300&h=300&fit=crop",
                Category = "bebes",
                Stock = 18,
                MinStock = 20,
                IsPromo = true,
                Description = "Cereal infantil fortificado con hierro y vitaminas para bebés a partir de 6 meses."
            },
            new() {
                Id = 27,
                Name = "Fideos Don Vittorio Spaguetti 1kg",
                Price = 5.90m,
                Image = "https://images.unsplash.com/photo-1474979266404-7eaacbcd87c5?w=300&h=300&fit=crop",
                Category = "abarrotes",
                Stock = 85,
                Description = "Fideos spaguetti Don Vittorio hechos con el mejor trigo durum."
            },
            new() {
                Id = 28,
                Name = "Chizitos Frito-Lay Fiesta 150g",
                Price = 5.50m,
                Image = "https://images.unsplash.com/photo-1566478989037-eec170784d0b?w=300&h=300&fit=crop",
                Category = "snacks",
                Stock = 45,
                Description = "Snack extruido sabor a queso, el favorito de las fiestas infantiles."
            }
        };
    }

    public List<Product> GetProducts() => _products;

    public List<Category> GetCategories() => _categories;

    public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public List<Product> GetRelatedProducts(Product product, int limit = 4)
    {
        return _products
            .Where(p => p.Category == product.Category && p.Id != product.Id)
            .Take(limit)
            .ToList();
    }

    public List<Product> GetDiscountedProducts()
    {
        return _products.Where(p => p.IsPromo && p.OriginalPrice.HasValue).ToList();
    }

    public string GetCategoryName(string categoryId)
    {
        return _categories.FirstOrDefault(c => c.Id == categoryId)?.Name ?? categoryId;
    }

    // Admin & Staff Operations
    public void AddProduct(Product product)
    {
        product.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        var existing = GetProductById(product.Id);
        if (existing != null)
        {
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.OriginalPrice = product.OriginalPrice;
            existing.Image = product.Image;
            existing.Category = product.Category;
            existing.Stock = product.Stock;
            existing.IsPromo = product.IsPromo;
            existing.Description = product.Description;
        }
    }

    public void DeleteProduct(int id)
    {
        var existing = GetProductById(id);
        if (existing != null)
        {
            _products.Remove(existing);
        }
    }
}
