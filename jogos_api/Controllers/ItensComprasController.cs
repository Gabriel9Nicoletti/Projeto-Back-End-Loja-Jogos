using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jogos_api.Data;
using jogos_api.Models;

namespace jogos_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensComprasController : ControllerBase
    {
        private readonly LojaJogosContext _context;

        public ItensComprasController(LojaJogosContext context)
        {
            _context = context;
        }

        // GET: api/ItensCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItensCompra>>> GetItensCompras()
        {
            return await _context.ItensCompras.ToListAsync();
        }

        // GET: api/ItensCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItensCompra>> GetItensCompra(int id)
        {
            var itensCompra = await _context.ItensCompras.FindAsync(id);

            if (itensCompra == null)
            {
                return NotFound();
            }

            return itensCompra;
        }

        // PUT: api/ItensCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItensCompra(int id, ItensCompra itensCompra)
        {
            if (id != itensCompra.ItemCompraId)
            {
                return BadRequest();
            }

            _context.Entry(itensCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItensCompraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItensCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItensCompra>> PostItensCompra(ItensCompra itensCompra)
        {
            _context.ItensCompras.Add(itensCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItensCompra", new { id = itensCompra.ItemCompraId }, itensCompra);
        }

        // DELETE: api/ItensCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItensCompra(int id)
        {
            var itensCompra = await _context.ItensCompras.FindAsync(id);
            if (itensCompra == null)
            {
                return NotFound();
            }

            _context.ItensCompras.Remove(itensCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItensCompraExists(int id)
        {
            return _context.ItensCompras.Any(e => e.ItemCompraId == id);
        }
    }
}
