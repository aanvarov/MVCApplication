using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebApplication.Models;
using Newtonsoft.Json;

namespace MVCWebApplication.Controllers
{
    public class ProductController : Controller
    {
        private const string BaseUrl = "https://localhost:5000/";
        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient{BaseAddress = new Uri(BaseUrl)};
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            var res = await _client.GetAsync("api/Product");
            if (!res.IsSuccessStatusCode) return View();
            var prResponse = res.Content.ReadAsStringAsync().Result;
            return View(JsonConvert.DeserializeObject<List<Product>>(prResponse));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            var res = await _client.GetAsync("api/Category");
            if (!res.IsSuccessStatusCode) return View();
            var prResponse = res.Content.ReadAsStringAsync().Result;
            var categories = JsonConvert.DeserializeObject<List<Category>>(prResponse);
            ViewData["ProductCategoryId"] = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ProductCategoryId")] Product product)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                var rndId = new Random();
                product.Id = rndId.Next(100);
                //HTTP POST
                var postTask = await _client.PostAsJsonAsync<Product>("api/Product",
                    product);
                if (postTask.IsSuccessStatusCode) return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            var res = await _client.GetAsync("api/Category");
            if (!res.IsSuccessStatusCode) return View();
            var prResponse = res.Content.ReadAsStringAsync().Result;
            var categories = JsonConvert.DeserializeObject<List<Category>>(prResponse);
            var product = await GetProductById(id);
            if (product == null) return NotFound();
            ViewData["ProductCategory"] = new SelectList(categories, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ProductCategoryId")] Product product)
        {
            if (id != product.Id) return NotFound();
            if (!ModelState.IsValid) return View();
            try
            {
                //HTTP POST
                var postTask = await _client.PutAsJsonAsync<Product>("api/Product/"+product.Id,
                    product);
                // postTask.Wait();
                // var result = postTask.Result;
                if (postTask.IsSuccessStatusCode) return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var res = await _client.DeleteAsync("api/Product/"+id);
            ModelState.AddModelError("Err", "Server Error. Please contact administrator.");
            if (!res.IsSuccessStatusCode) return View();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            var cat = GetProductById(id);
            return id != cat.Id;
        }
        
        public async Task<Product> GetProductById(int id)
        {
            var res = await _client.GetAsync("api/Product/"+id);
            if (!res.IsSuccessStatusCode) return null;
            var prResponse = res.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Product>(prResponse);
        } 
    }
}
