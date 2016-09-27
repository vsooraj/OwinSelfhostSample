using Common;
using miGuardClient.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace miGuardClient.Controllers
{
    public class OperationsController : Controller
    {
        HttpClient client;
        string url = Appsettings.AppSetting("hostUrl");
        // GET: Operations
        public OperationsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url + "operations");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var operations = JsonConvert.DeserializeObject<List<Operation>>(responseData);

                return View(operations);
            }
            return View("Error");
        }

        // GET: Operations/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var operation = JsonConvert.DeserializeObject<Operation>(responseData);

                return View(operation);
            }
            return View("Error");

        }

        // GET: Operations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Operations/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Operations/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Operations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Operations/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Operations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
