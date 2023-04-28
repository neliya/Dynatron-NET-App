using System;
using CustomerNetApp.Data;
using CustomerNetApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerNetApp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomersController : ControllerBase
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public CustomersController(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;

		}

        [HttpGet]
		public async Task<IActionResult> GetAllCustomers()
		{
			var customers =  await _applicationDbContext.Customers.ToListAsync();
			return Ok(customers);
		}

		[HttpPost]
		public async Task<IActionResult> AddCustomer([FromBody] Customer customerRequest)
		{
			customerRequest.Id = Guid.NewGuid();
			await _applicationDbContext.Customers.AddAsync(customerRequest);
			await _applicationDbContext.SaveChangesAsync();

			return Ok(customerRequest);
		}

		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
		{
			var customer = await _applicationDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
			if (customer == null) {
				return NotFound();
			}
			return Ok(customer);
		}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, Customer updateCustomerRequest)
        {
            var customer = await _applicationDbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
			customer.FirstName = updateCustomerRequest.FirstName;
			customer.LastName = updateCustomerRequest.LastName;
			customer.Phone = updateCustomerRequest.Phone;
			customer.Email = updateCustomerRequest.Email;
			customer.CreatedDate = updateCustomerRequest.CreatedDate;
			customer.LastUpdated = DateTime.UtcNow;

			await _applicationDbContext.SaveChangesAsync();
			return Ok(customer);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id) {
            var customer = await _applicationDbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
			_applicationDbContext.Customers.Remove(customer);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(customer);
        }
    }
}

