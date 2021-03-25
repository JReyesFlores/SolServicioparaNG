using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private HelperServiceContext _context;
        public ProductController(HelperServiceContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var lista = await _context.Products.ToListAsync();
            return lista;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Get([FromRoute] string id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.id == id);
            if (product == null) return BadRequest();
            return product;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Post), new { id = product.id }, product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Update([FromBody] Product product, [FromRoute] string id)
        {
            var productDb = await _context.Products.SingleOrDefaultAsync(x => x.id.Equals(id));
            if (productDb != null)
            {
                productDb.image = product.image == null ? productDb.image : product.image;
                productDb.price = product.price == 0 ? productDb.price : product.price;
                productDb.title = product.title == null ? productDb.title : product.title;
                productDb.description = product.description == null ? productDb.description : product.description;

                await _context.SaveChangesAsync();
                return productDb;
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Delete([FromRoute] string id)
        {
            try
            {
                var productDb = await _context.Products.SingleOrDefaultAsync(x => x.id.Equals(id));
                if (productDb != null)
                {
                    _context.Products.Remove(productDb);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
