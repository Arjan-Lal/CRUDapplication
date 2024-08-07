using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDwebAPI.Data;
using CRUDwebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDwebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            var customersList = _context.Customers.ToList();

            if (customersList.Count == 0)
            {
                return NotFound("No customers available");
            }
            return Ok(customersList);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound($"Customer {id} not found");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Customer model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Customer created successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Customer model)
        {
            if (model == null || model.CustomerID == 0)
            {
                if (model == null)
                {
                    return BadRequest("Customer data is invalid");
                }
                else if (model.CustomerID == 0)
                {
                    return BadRequest($"Customer {model.CustomerID} is invalid");
                }
            }
            try
            {
                var customer = _context.Customers.Find(model.CustomerID);
                if (customer == null)
                {
                    return NotFound($"Customer {model.CustomerID} not found");
                }
                customer.FirstName = model.FirstName;
                customer.Surname = model.Surname;
                _context.SaveChanges();
                return Ok("Customer details updated successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);

                if (customer == null)
                {
                    return NotFound($"Customer {id} not found");
                }
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return Ok("Customer details deleted successully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
