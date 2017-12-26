using MvcTietokantaAzure.Models;
using Newtonsoft.Json;
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
            ViewBag.OmaTieto = "Testi";

            KoulukantaEntities entities = new KoulukantaEntities();
            List<HENKILOT> model = entities.HENKILOT.ToList();
            entities.Dispose();

            return View(model);
        }

        public ActionResult Index2()
        {
     
            return View();
        }

        public ActionResult Index3()
        {

            return View();
        }

        public JsonResult GetList()
        {
            KoulukantaEntities entities = new KoulukantaEntities();
           
            var model = (from h in entities.HENKILOT
                         select new
                         { 
                            HenkiloID = h.HenkiloID,
                            Etunimi = h.Etunimi,
                            Sukunimi = h.Sukunimi,
                            Osoite = h.Osoite,
                            Esimies= h.Esimies
                         }).ToList();
                        

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

          //  Response.Expires = -1;
           // Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSingleHenkilo(int id)
        {
            KoulukantaEntities entities = new KoulukantaEntities();
            string s = id.ToString();
            var model = (from h in entities.HENKILOT
                         where h.HenkiloID == id
                         select new
                         {
                             HenkiloID = h.HenkiloID,
                             Etunimi = h.Etunimi,
                             Sukunimi = h.Sukunimi,
                             Osoite = h.Osoite,
                             Esimies = h.Esimies
                         }).FirstOrDefault();


            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            //  Response.Expires = -1;
            // Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
       
}