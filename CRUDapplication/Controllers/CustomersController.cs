using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDapplication.Data;
using CRUDapplication.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace CRUDapplication.Controllers
{
    public class CustomersController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44374/api/");

        private readonly HttpClient _client;

        public CustomersController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // GET: Customers
        [HttpGet]
        public IActionResult Index()
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Customer/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customerList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }
            return View(customerList);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = new CustomerViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Customer/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
            }
            if (customer == null || customer.CustomerID != id)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Customer/Post", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
            return View();
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            CustomerViewModel customer = new CustomerViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Customer/Get/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
            }    
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Customer/Put/" + model.CustomerID, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            CustomerViewModel customer = new CustomerViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Customer/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "Customer/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        private bool CustomerExists(int id)
        {
            //return _context.Customer.Any(e => e.CustomerID == id);
            return true;
        }
    }
}
