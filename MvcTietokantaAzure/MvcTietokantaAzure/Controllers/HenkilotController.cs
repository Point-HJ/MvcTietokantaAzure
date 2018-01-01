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

          Response.Expires = -1;
          Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSingleHenkilo(string id) //Pitää muuttaa stringiksi!?
        {
            KoulukantaEntities entities = new KoulukantaEntities();

            var model = (from h in entities.HENKILOT
                         where h.HenkiloID == id
                         select new
                         {
                             HenkiloID = h.HenkiloID,
                             Etunimi = h.Etunimi,
                             Sukunimi = h.Sukunimi,
                             Osoite = h.Osoite,
                             Esimies = h.Esimies
                         }
                        
                         ).FirstOrDefault();
            
       


            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Update(HENKILOT henk)
        {
            KoulukantaEntities entities = new KoulukantaEntities();
            string id = henk.HenkiloID;

            bool OK = false;

            // onko kyseessä muokkaus vai uuden lisääminen?
            if (id == "(uusi)")
            {
                // kyseessä on uuden asiakkaan lisääminen, kopioidaan kentät
                HENKILOT dbItem = new HENKILOT()
                {
                    HenkiloID = henk.HenkiloID,
                    Etunimi = henk.Etunimi,
                    Sukunimi = henk.Sukunimi,
                    Osoite = henk.Osoite,
                    Esimies = henk.Esimies
                };

                // tallennus tietokantaan
                entities.HENKILOT.Add(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            else
            {
                // muokkaus, haetaan id:n perusteella riviä tietokannasta
                HENKILOT dbItem = (from c in entities.HENKILOT
                                   where c.HenkiloID == id
                                   select c).FirstOrDefault();
                if (dbItem != null)
                {
                    dbItem.Etunimi = henk.Etunimi;
                    dbItem.Sukunimi = henk.Sukunimi;
                    dbItem.Osoite = henk.Osoite;
                    dbItem.Esimies = henk.Esimies;


                    // tallennus tietokantaan
                    entities.SaveChanges();
                    OK = true;
                }
            }
            entities.Dispose();

            return Json(OK, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id)
        {
            KoulukantaEntities entities = new KoulukantaEntities();

            // etsitään id:n perusteella asiakasrivi kannasta
            bool OK = false;
            HENKILOT dbItem = (from h in entities.HENKILOT
                               where h.HenkiloID == id
                               select h).FirstOrDefault();
            if (dbItem != null)
            {
                // tietokannasta poisto
                entities.HENKILOT.Remove(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            entities.Dispose();

            return Json(OK, JsonRequestBehavior.AllowGet);
        }

    }
       
}