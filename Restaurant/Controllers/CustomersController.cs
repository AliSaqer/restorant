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
    public class CustomersController : ControllerBase
    {
        private readonly RestaurantdbContext _context;

        public CustomersController(RestaurantdbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            //if (_context.Customers == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Customers.ToListAsync();

            var rsl = await _context.Customers.Where<Customer>(x => x.Archived == 0).ToListAsync();
            return Ok(rsl);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'RestaurantdbContext.Customers'  is null.");
            }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult AddOrder (int CostomerId, int restaurantMenuID)
        {
         var cus = _context.Customers.Where(e => e.Id == CostomerId);
            if (!cus.Any()) {
                return NotFound(); 
            }

            var menu = _context.Restaurantmenus.Where(e => e.Id == restaurantMenuID);
            if (!menu.Any())
            {
                return NotFound();
            }

            _context.CustomerRestaurantmenus.Add(new CustomerRestaurantmenu { CustomerId = CostomerId, RestaurantmenuId = restaurantMenuID });
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int CostomerId, int restaurantMenuID)
        {
            var cus = _context.Customers.Where(e => e.Id == CostomerId);
            if (!cus.Any())
            {
                return NotFound();
            }

            var menu = _context.Restaurantmenus.Where(e => e.Id == restaurantMenuID);
            if (!menu.Any())
            {
                return NotFound();
            }

            _context.CustomerRestaurantmenus.Add(new CustomerRestaurantmenu { CustomerId = CostomerId, RestaurantmenuId = restaurantMenuID });
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IActionResult delete(int CostomerId, int restaurantMenuID)
        {
            var cus = _context.Customers.Where(e => e.Id == CostomerId);
            if (!cus.Any())
            {
                return NotFound();
            }

            var menu = _context.Restaurantmenus.Where(e => e.Id == restaurantMenuID);
            if (!menu.Any())
            {
                return NotFound();
            }

            _context.CustomerRestaurantmenus.Add(new CustomerRestaurantmenu { CustomerId = CostomerId, RestaurantmenuId = restaurantMenuID });
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Cabtalize()
        {
            var dbCustomers = await _context.Customers.Select(x => new
            {
                FirstName = _Cabtalize(x.FirstName),
                LastName = _Cabtalize(x.LastName)
            }).ToListAsync();

           return Ok(dbCustomers);

        }

        private string _Cabtalize (string target)
        {
            if (String.IsNullOrEmpty(target))
            {
                return target;
            }

            return target.First().ToString().ToUpper() + target.Substring(1); ;
        }
    }
}
