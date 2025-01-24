using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Model;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Product.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly RepositoryContext _context;

    public ValuesController(RepositoryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get(int categoryId, int productDetailId)
    {
        string productFilter = $"ProductDetailResponse.ProductDetailId == {productDetailId}"; 
        string categoryFilter = $"CategoryResponse.CategoryId == {categoryId}";

        IQueryable<ProductResponse> query = default;

        query = _context.Products.Select(p => new ProductResponse
        {
                ProductId = p.ProductId,
                ProductName = p.Name,
                CategoryResponse = new CategoryResponse()
                {
                    CategoryId = p.CategoryId,
                    CategoryName = "N/A", // Default for Category
                }
            })
            .AsQueryable();

        if (categoryId != 0)
        {
            query = query.Join(
                _context.Categories,
                p => p.CategoryResponse.CategoryId,
                c => c.CategoryId,
                (p, c) => new ProductResponse
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryResponse = new CategoryResponse()
                    {
                        CategoryId = p.CategoryResponse.CategoryId,
                        CategoryName = "N/A", // Default for Category
                    }
                }
            ).Where(categoryFilter).AsQueryable();
        }

        if (productDetailId != 0)
        {
            query = query.Join(
                _context.ProductDetails,
                p => p.ProductId,
                c => c.ProductId,
                (p, c) => new ProductResponse
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryResponse = new CategoryResponse()
                    {
                        CategoryId = p.CategoryResponse.CategoryId,
                        CategoryName = "N/A", // Default for Category
                    },
                    ProductDetailResponse = new ProductDetailResponse()
                    {
                        ProductDetailId = c.ProductDetailId,
                    }
                }
            ).Where(productFilter).AsQueryable();
        }

        return Ok(query.ToList());
    }
}
