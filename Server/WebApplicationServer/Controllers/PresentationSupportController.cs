using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationServer.Controllers {

    public class PresentationSupportController : Controller {

        // GET: PresentationSupport
        public ActionResult Index() {
            return View();
        }

        // GET: PresentationSupport/RealtimeReaction
        public ActionResult RealtimeReaction() {
            return View();
        }

    }

}