using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class GoogleMapsController : BaseController
    {
        // GET: GoogleMaps
        public ActionResult SearchMapModal()
        {
            return PartialView();
        }

        // GET: GoogleMaps
        public ActionResult SearchMap()
        {
            return PartialView();
        }

        // GET: GoogleMaps
        public ActionResult AutoCompleteAddressFormModal()
        {
            return PartialView();
        }

        // GET: GoogleMaps
        public ActionResult AutoCompleteAddressForm()
        {
            return PartialView();
        }

        public ActionResult AutoCompleteAddress(string id)
        {
            ViewBag.InputId = id;
            //ViewBag.Altura = $"{id}_Ciudad";
            //ViewBag.Calle = $"{id}_Calle";
            //ViewBag.Ciudad = $"{id}_Ciudad";
            //ViewBag.Provincia = $"{id}_Provincia";
            //ViewBag.Pais = $"{id}_Pais";
            //ViewBag.CodigoPostal = $"{id}_CodigoPostal";

            return PartialView();
        }



        // GET: GoogleMaps/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GoogleMaps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GoogleMaps/Create
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

        // GET: GoogleMaps/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GoogleMaps/Edit/5
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

        // GET: GoogleMaps/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GoogleMaps/Delete/5
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
