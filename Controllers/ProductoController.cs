using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; 
using System.Linq;
using System.Threading.Tasks;
using ApiProducto.Data;
using ApiProducto.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;


namespace ApiProducto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {

        private readonly DataContext _context;
        public ProductoController(DataContext context)
        {
            _context = context;
        }
        // HttpPOST method
            [HttpPost]
            public async Task<ActionResult<Producto>> PostProducto(Producto producto) 
            {
                var ProductoNew = new Producto 
                {
                    Nombre = producto.Nombre,
                    Description = producto.Description,
                    Precio = producto.Precio,
                    FechaAlta = DateTime.Now,
                    Activo = true
                };

                _context.Productos.Add(ProductoNew);
                await _context.SaveChangesAsync();

                return ProductoNew;
            }
        
        
        // HttpGET method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos() 
        {
            return await _context.Productos.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id) 
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null) 
            {
                return NotFound();
            }

            return producto;
        }
        // HttpPUT method
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)  
        {
            if (id!= producto.Id) 
            {
                return BadRequest();
            }

            var productoEdit = await _context.Productos.FindAsync(id);
            if(productoEdit == null) 
            {
                return NotFound();
            }

            productoEdit.Nombre = producto.Nombre;
            productoEdit.Description = producto.Description;
            productoEdit.Precio = producto.Precio;
            productoEdit.Activo = producto.Activo;

            await _context.SaveChangesAsync();

            return Ok();
        }
        
        // HttpDELETE method
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id) 
        {
            var productoDelete = await _context.Productos.FindAsync(id);
            if (productoDelete == null) 
            {
                return NotFound();
            }

            _context.Productos.Remove(productoDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
    }
}