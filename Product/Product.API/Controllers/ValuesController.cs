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
        string productFilter = $"ProductDetailId == {productDetailId}"; 
        string categoryFilter = $"CategoryId == {categoryId}";

        IQueryable<ProductResponse> query = default;

        query = _context.Products.Select(p => new ProductResponse
        {
                ProductId = p.ProductId,
                ProductName = p.Name,
                CategoryId = p.CategoryId,
                CategoryName = "N/A", // Default for Category
            })
            .AsQueryable();

        if (categoryId != 0)
        {
            query = query.Join(
                _context.Categories.Where(categoryFilter),
                p => p.CategoryId,
                c => c.CategoryId,
                (p, c) => new ProductResponse
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryName = c.Name,
                    CategoryId = c.CategoryId
                }
            ).AsQueryable();
        }

        if (productDetailId != 0)
        {
            query = query.Join(
                _context.ProductDetails.Where(productFilter),
                p => p.ProductId,
                c => c.ProductId,
                (p, c) => new ProductResponse
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryName = p.CategoryName,
                    CategoryId = p.CategoryId,
                    ProductDetailId = c.ProductDetailId
                }
            ).AsQueryable();
        }

        return Ok(query.ToList());
    }
}
