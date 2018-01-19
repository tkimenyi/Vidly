using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //Get /api/customers
        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.Customers.Include(c => c.MembershipType).ToList());
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" +customer.Id), customer );
        }

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                return NotFound();
            }

            // we can use a tool like automapper to do this mapping for us
            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipType.Name = customer.MembershipType.Name;
            customerInDb.MembershipType.DiscountRate = customer.MembershipType.DiscountRate;
            customerInDb.MembershipType.DurationInMonths = customer.MembershipType.DurationInMonths;
            customerInDb.MembershipType.SignUpFee = customer.MembershipType.SignUpFee;

            _context.SaveChanges();

            return Ok(customerInDb);
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDb);
            return Ok(_context.SaveChanges());
        }
    }
}
