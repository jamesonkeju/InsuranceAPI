using Insurance.Services.AxaMansard.DTO;
using Insurance.Utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class MarketPlaceController : Controller
    {
        public MarketPlaceController()
        {

        }
        // GET: MarketPlaceController
        public async Task<ActionResult> Index()
        {
          
            return View();
        }

        public async Task<ActionResult> ManageProduct()
        {
            var products = new List<Insurance.Data.Models.Domains.ProductList>();

            var msg = new ApiResult<AxaMansardDTO.GetProductResponse.Root>();

            var response = await MiddleWare.PortalPostBasicAsync(null, System.Threading.CancellationToken.None, ApplicationURL.GetProductList);

            if (response == null)
            {
                return View(products);

            }
            var result = JsonConvert.DeserializeObject<Presentation.Models.productobject.Root>(response);
           

            if (result.isSuccessful != true)
            {
               
                return View(products);

            }
          

            foreach (var item in result.result.returnedObject)
            {
                var data = new Insurance.Data.Models.Domains.ProductList
                {
                    description = item.description,
                    duration = item.duration,
                    price = item.price,
                    IsActive = true,
                    IsDeleted = false,
                    rate = item.rate,
                    productCode = item.productCode,
                    CreatedBy = "System"
                };
                products.Add(data);

              
            }

            return View(products);
        }

        // GET: MarketPlaceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MarketPlaceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarketPlaceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarketPlaceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MarketPlaceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarketPlaceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarketPlaceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
