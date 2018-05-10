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
            return PartialView();
        }

    }
}
