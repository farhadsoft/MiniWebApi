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

    public Product? GetProductById(Guid id)
    {
        return products.SingleOrDefault(x => x.Id == id);
    }

    public void Update(Product product)
    {
        var item = products.Single(x => x.Id == product.Id);
        products.Remove(item);
        products.Add(product);
    }
}