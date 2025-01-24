namespace Product.Api;

public class ProductResponse
{
    public int ProductId { get; set; }
    public int ProductDetailId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
}
