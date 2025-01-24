namespace Product.Api;

public class ProductResponse
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public CategoryResponse CategoryResponse { get; set; } = new CategoryResponse();
    public ProductDetailResponse ProductDetailResponse { get; set; } = new ProductDetailResponse();
}


public class CategoryResponse
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}

public class ProductDetailResponse
{
    public int ProductDetailId { get; set; }
}