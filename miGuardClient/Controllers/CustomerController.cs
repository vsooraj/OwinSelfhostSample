using miGuardClient.Models;
using miGuardClient.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace miGuardClient.Controllers
{
    public class CustomerController : Controller
    {
           HttpClient client;
        //The URL of the WEB API Service
        string url = Appsettings.AppSetting("hostUrl");
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public CustomerController( )
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url+"companies");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: CustomerInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var customers = JsonConvert.DeserializeObject<List<Customer>>(responseData);

                return View(customers);
            }
            return View("Error");
        }
        public ActionResult Create()
        {
            return View(new Customer());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, customer);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<Customer>(responseData);

                return View(Employee);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Customer customer)
        {

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + id, customer);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<Customer>(responseData);

                return View(Employee);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Customer customer)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

    }
}