using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //ben bu isteği yaptığımda bu insanlar bana nasıl ulaşsın ?? /api/Products ile ulaşabilirsin demek.
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //Loosely copied
        //naming convention
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet("merhaba")]

        public string Get()
        {
            return "Merhaba";
        }

        [HttpGet("listProducts")]
        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product{ ProductId=1, ProductName="Bere"},
                new Product{ ProductId=2, ProductName="Elma"},
                new Product{ ProductId=3, ProductName="Radyo"},
                new Product{ ProductId=4, ProductName="Battaniye"},
                new Product{ ProductId=5, ProductName="Anahtar"},
                new Product{ ProductId=6, ProductName="Takı"},

            };
        }

        [HttpGet("productlist")]
        public List<Product> GetDeneme()
        {
            //IProductService productService = new ProductManager(new EfProductDal());
            var result = _productService.GetAll();
            return result.Data;
        }

        [HttpGet("getall2")]
        public string GetDeneme2()
        {
            var result = _productService.GetAll();
            return result.Message;

        }

        [HttpGet("getall")]
        public IActionResult GetDeneme3()
        {
            var result = _productService.GetAll();

            //eğer sonuç başarılı dönerse Ok() başarılı oldu demek
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult PostDeneme1(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetDeneme4(int id)
        {
            var result = _productService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }




    }
}
