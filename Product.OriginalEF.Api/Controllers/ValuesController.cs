using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;

namespace Product.OriginalEF.Api.Controllers;

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
        //string productFilter = $"ProductDetailResponse.ProductDetailId == {productDetailId}"; 
        //string categoryFilter = $"CategoryResponse.CategoryId == {categoryId}";
        //string crossFilter = $"ProductDetailResponse.ProductDetailId == {productDetailId} && CategoryResponse.CategoryId == {categoryId}";

        var predicate = PredicateBuilder.True<ProductResponse>();

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
            predicate = predicate.And(x => x.CategoryResponse.CategoryId == categoryId);

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
            )
                //.Where(categoryFilter)
                .AsQueryable();
        }

        if (productDetailId != 0)
        {
            predicate = predicate.And(x => x.ProductDetailResponse.ProductDetailId == productDetailId);

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
            ).AsQueryable();
        }

        //return Ok(query.Where(crossFilter).ToList());
        return Ok(query.Where(predicate).ToList());
    }
}


// Lớp hỗ trợ để kết hợp Expression
public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> True<T>() => f => true;
    public static Expression<Func<T, bool>> False<T>() => f => false;

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(expr1.Body, expr2.Body), expr1.Parameters);
    }
}