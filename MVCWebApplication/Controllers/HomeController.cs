using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCWebApplication.Models;
using Newtonsoft.Json;

namespace MVCWebApplication.Controllers
{
    public class HomeController : Controller
    {

        //GET: Home
        public async Task<ActionResult> Index()
        {
            //Hosted web API REST Service base url
            const string baseUrl = "https://localhost:5001/";

            var prodInfo = new List<Product>();
            using var client = new HttpClient();
            //Passing service base url
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            //Define request data format
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            //Sending request to find web api REST service resource GetAllProducts using HttpClient
            var res = await client.GetAsync("api/Product");
            //Checking the response is successful or not which is sent using HttpClient
            if (res.IsSuccessStatusCode)
            {
                //Storing the response details received from web api
                var prResponse = res.Content.ReadAsStringAsync().Result;
                //Deserializing the response received from web api and storing into the Product list
                prodInfo = JsonConvert.DeserializeObject<List<Product>>(prResponse);
            }
            //returning the Product list to view
            return View(prodInfo);
        }
        
        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            const string baseurl = "https://localhost:5001/";
            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                var res = await client.GetAsync("api/Product/"+id);
                //Checking the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var prResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response received from web api and storing into the Product list
                        product = JsonConvert.DeserializeObject<Product>(prResponse);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}