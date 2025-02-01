using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Model;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

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
    public IActionResult Get(int categoryId, int productDetailId, int productId)
    { 
        List<int> productIds = new() { 1, 2, 3, 4, 5 };
        var includedFilter = $"((CategoryResponse.CategoryId == {categoryId}) && ((ProductDetailResponse.ProductDetailId != null) && (ProductDetailResponse.ProductDetailId != 0)))";
        var excludedFilter = $"((ProductId in ({string.Join(",", productIds)})) || ((ProductDetailResponse.ProductDetailId != null) && (ProductDetailResponse.ProductDetailId == 7)))";
        //var e = $"((CategoryResponse.CategoryId Eq `2`))";

        IQueryable<ProductResponse> query = default;

        query = _context.Products
            .Select(p => new ProductResponse
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

        if (includedFilter.Contains(nameof(ProductResponse.CategoryResponse)))
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
                })
                .AsQueryable();
        }

        if (includedFilter.Contains(nameof(ProductResponse.ProductDetailResponse)))
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
                })
               .AsQueryable();
        }

        var result = query.Where(includedFilter).Where($"not {excludedFilter}").ToList();

        return Ok(result);
    }

    public Dictionary<string, object> GetPropertiesAsDictionary<T>(T obj)
    {
        var dictionary = new Dictionary<string, object>();
        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            object value = property.GetValue(obj);
            if (value != null && !property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
            {
                // Nếu là đối tượng không phải primitive, đệ quy vào
                var nestedProperties = GetPropertiesAsDictionary(value);
                foreach (var nestedKvp in nestedProperties)
                {
                    dictionary[$"{property.Name}.{nestedKvp.Key}"] = nestedKvp.Value; // Thêm thuộc tính theo định dạng
                }
            }
            else
            {
                // Nếu là primitive hoặc string
                dictionary[property.Name] = value;
            }
        }
        return dictionary;
    }

}