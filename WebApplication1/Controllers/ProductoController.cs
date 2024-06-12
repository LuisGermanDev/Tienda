using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private TiendaContext _context;
        public ProductoController(TiendaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductoDto>>GetProductos()=>
            await _context.Productos.Select(p=> new ProductoDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ProductoId=p.ProductoId,
            }).ToListAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>>GetbyId(int id)
        {
            var product = await _context.Productos.FindAsync(id);
            if (product==null)
            {
                return NotFound();
            }
            var productdto = new ProductoDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductoId = product.ProductoId,
            };
            return Ok(productdto);
        }
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Add(ProductoInsertDto productoInsertDto)
        {
            var product = new Producto()
            {
                Name = productoInsertDto.Name,
                Price = productoInsertDto.Price,
                ProductoId = productoInsertDto.ProductoId
            };
            await _context.Productos.AddAsync(product);
            await _context.SaveChangesAsync();
            var productdto = new ProductoDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductoId = product.ProductoId,
            };
            return CreatedAtAction(nameof(GetbyId), new{id = product.ProductoId}, productdto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Producto>> Update(int id, ProductoUpdateDto productoUpdateDto) 
        {
            var product = await _context.Productos.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = productoUpdateDto.Name;
            product.Price = productoUpdateDto.Price;
            product.ProductoId = productoUpdateDto.ProductoId;
            await _context.SaveChangesAsync();

            var productdto = new ProductoDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductoId = product.ProductoId,
            };

            return  Ok(productdto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>Delect(int id)
        {
            var product = await _context.Productos.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Productos.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
