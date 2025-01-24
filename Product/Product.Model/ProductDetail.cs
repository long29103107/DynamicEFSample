namespace Product.Model;

public class ProductDetail
{
    public int ProductDetailId { get; set; }
    public string Description { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}

