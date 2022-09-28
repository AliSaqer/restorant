using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.DataObjects;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantmenusController : ControllerBase
    {
        private readonly RestaurantdbContext _context;

        public RestaurantmenusController(RestaurantdbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurantmenus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurantmenu>>> GetRestaurantmenus()
        {
          if (_context.Restaurantmenus == null)
          {
              return NotFound();
          }
            return await _context.Restaurantmenus.ToListAsync();
        }

        // GET: api/Restaurantmenus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurantmenu>> GetRestaurantmenu(int id)
        {
          if (_context.Restaurantmenus == null)
          {
              return NotFound();
          }
            var restaurantmenu = await _context.Restaurantmenus.FindAsync(id);

            if (restaurantmenu == null)
            {
                return NotFound();
            }

            return restaurantmenu;
        }

        // PUT: api/Restaurantmenus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantmenu(int id, Restaurantmenu restaurantmenu)
        {
            if (id != restaurantmenu.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurantmenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantmenuExists(id))
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

        // POST: api/Restaurantmenus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurantmenu>> PostRestaurantmenu(Restaurantmenu restaurantmenu)
        {
          if (_context.Restaurantmenus == null)
          {
              return Problem("Entity set 'RestaurantdbContext.Restaurantmenus'  is null.");
          }
            _context.Restaurantmenus.Add(restaurantmenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantmenu", new { id = restaurantmenu.Id }, restaurantmenu);
        }

        // DELETE: api/Restaurantmenus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantmenu(int id)
        {
            if (_context.Restaurantmenus == null)
            {
                return NotFound();
            }
            var restaurantmenu = await _context.Restaurantmenus.FindAsync(id);
            if (restaurantmenu == null)
            {
                return NotFound();
            }

            _context.Restaurantmenus.Remove(restaurantmenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantmenuExists(int id)
        {
            return (_context.Restaurantmenus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpGet("{resturantMenuId}")]
        public ActionResult<Restaurantmenu> IsResturantMenueAvaialbel(int resturantMenuId)
        {
            if (resturantMenuId == 0)
            {
                return NotFound();
            }
            var dbQuery = _context.Restaurantmenus.Where(e => e.Id == resturantMenuId && e.Quantity > 0);

            if (dbQuery.Any())
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
