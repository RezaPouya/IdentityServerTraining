using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessing.Product.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository customerRepository)
        {
            _productRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllCustomers()
        {
            return await _productRepository.GetAllProducts();
        }
    }
}