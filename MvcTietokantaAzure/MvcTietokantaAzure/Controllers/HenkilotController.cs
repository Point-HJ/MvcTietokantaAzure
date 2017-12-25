using MvcTietokantaAzure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTietokantaAzure.Controllers
{
    public class HenkilotController : Controller
    {
        // GET: Henkilot
        public ActionResult Index()
        {
            ViewBag.OmaTieto = "Höpölöpö";

            KoulukantaEntities entities = new KoulukantaEntities();
            List<HENKILOT> model = entities.HENKILOT.ToList();
            entities.Dispose();

            return View(model);
        }
    }


    public class HenkilotController : Controller
    {
        // GET: Henkilot
        public ActionResult Index()
        {
            ViewBag.OmaTieto = "Höpölöpö";

            KoulukantaEntities entities = new KoulukantaEntities();
            List<HENKILOT> model = entities.HENKILOT.ToList();
            entities.Dispose();

            return View(model);
        }
    }
}