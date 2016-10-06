using Common;
using miGuardClient.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace miGuardClient.Controllers
{
    public class ItemsController : Controller
    {
        HttpClient client;
        string url = Appsettings.AppSetting("hostUrl");
        public ItemsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url + "items");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Item
        public async Task<ActionResult> Index(string sortOrder, string searchString, int? pageNo, int? pageSize)
        {
            ViewBag.RequestTypeSortParm = String.IsNullOrEmpty(sortOrder) ? "requestType_desc" : "";

            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var items = JsonConvert.DeserializeObject<List<Item>>(responseData);

                switch (sortOrder)
                {
                    case "requestType_desc":
                        items = items.OrderByDescending(s => s.requestType).ToList();
                        break;
                    //case "requestType_desc":
                    //    items = items.OrderByDescending(s => s.requestType).ToList();
                    //    break;
                    //case "requestType_desc":
                    //    items = items.OrderByDescending(s => s.requestType).ToList();
                    //    break;
                    //case "requestType_desc":
                    //    items = items.OrderByDescending(s => s.requestType).ToList();
                    //    break;
                    //case "requestType_desc":
                    //    items = items.OrderByDescending(s => s.requestType).ToList();
                    //    break;
                    //case "requestType_desc":
                    //    items = items.OrderByDescending(s => s.requestType).ToList();
                    //    break;
                    default:
                        items = items.OrderBy(s => s.requestType).ToList();
                        break;
                }
                int temPageSize = 3;
                temPageSize = pageSize.HasValue ? pageSize.Value : temPageSize;
                items = items.Take(temPageSize).Skip(1).ToList();

                return View(items);
            }
            return View("Error");
        }

        // GET: Item/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var item = JsonConvert.DeserializeObject<Item>(responseData);

                return View(item);
            }
            return View("Error");

        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
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

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Item/Edit/5
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

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Item/Delete/5
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
