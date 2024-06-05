using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;
using graffino_online_store_api.System.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Products.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ProductsApiController : ControllerBase
{
    #region QUERY ENDPOINTS

    [HttpGet("all-categories")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Category>))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<Category>>> GetAllCategories();
    
    [HttpGet("all-products")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Product>))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<Product>>> GetAllProducts();

    [HttpGet("categories/{categoryId}/products")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Product>))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId);

    [HttpGet("category/{categoryId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Category))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Category>> GetCategoryById(int categoryId);
    
    [HttpGet("product/{productId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Product))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Product>> GetProductById(int productId);

    [HttpGet("filter-products")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Product>))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<FilterProductsResponse>> FilterProducts(
        [FromQuery] int? categoryId,
        [FromQuery] string? search,
        [FromQuery] Dictionary<string, string> properties,
        [FromQuery] int? page,
        [FromQuery] int? itemsPerPage,
        [FromQuery] string? sort
    );

    [HttpGet("filter-criteria/{categoryId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Dictionary<string, List<string>>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Dictionary<string, List<string>>>> FilterCriteria([FromRoute] int categoryId);
    
    #endregion

    #region COMMAND ENDPOINTS

    [HttpPost("create-category"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 201, type: typeof(Category))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryRequest request);

    [HttpPost("create-product"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 201, type: typeof(Product))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductRequest request);

    [HttpPut("update-category"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 200, type: typeof(Category))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Category>> UpdateCategory([FromBody] UpdateCategoryRequest request);

    [HttpPut("update-product"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 200, type: typeof(Product))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Product>> UpdateProduct([FromBody] UpdateProductRequest request);

    [HttpDelete("delete-category/{categoryId}"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 200, type: typeof(Category))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Category>> DeleteCategoryById(int categoryId);

    [HttpDelete("delete-product/{productId}"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 200, type: typeof(Product))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Product>> DeleteProductById(int productId);
    
    #endregion
}