var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/products", (ProductRepository repository) =>
{
    return Results.Ok(repository.GetAllProducts());
});

app.MapGet("/product/{id:Guid}", (ProductRepository repository, Guid Id) =>
{
    return Results.Ok(repository.GetProductById(Id));
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