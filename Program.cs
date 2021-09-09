var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.MapGet("/products", (ProductRepository repository) =>
{
    return repository.GetAllProducts();
});

app.MapGet("/product/{id:Guid}", (ProductRepository repository, Guid Id) =>
{
    return repository.GetProductById(Id);
});

app.MapPost("/product/", (ProductRepository repository, Product product) =>
{
    repository.Create(product);
    return Results.Created($"/product/{product.Id}", product);
});

app.MapDelete("/product/{id:Guid}", (ProductRepository repository, Guid Id) => 
{
    repository.Delete(Id);
    return Results.Ok();
});

app.MapPut("/product/", (ProductRepository repository, Product product) =>
{
    repository.Update(product);
    return Results.Ok();
});

app.Run();

record Product(Guid Id, string Name);

class ProductRepository
{
    private List<Product> products = new();

    public void Create(Product product)
    {
        if (product is null)
        {
            return;
        }
        
        products.Add(product);
    }

    public void Delete(Guid id)
    {
        var item = products.Single(x => x.Id == id);
        products.Remove(item);
    }

    public List<Product> GetAllProducts()
    {
        return products;
    }

    public Product GetProductById(Guid id)
    {
        return products.Single(x => x.Id == id);
    }

    public void Update(Product product)
    {
        var item = products.Single(x => x.Id == product.Id);
        products.Remove(item);
        products.Add(product);
    }
}