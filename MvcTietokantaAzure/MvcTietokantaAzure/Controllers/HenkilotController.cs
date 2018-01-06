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
                             Esimies = h.Esimies
                         }).ToList();


            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            Response.Expires = -1;
            Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSingleHenkilo(int id)
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
                         }).FirstOrDefault();


            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult update(HENKILOT henk)
        {

            KoulukantaEntities entities = new KoulukantaEntities();

            bool OK = false;

            // onko kyseessä muokkaus vai uuden lisääminen?
            if (henk.HenkiloID == 0)
            {
                // kyseessä on uuden asiakkaan lisääminen, kopioidaan kentät
                HENKILOT dbItem = new HENKILOT()
                {
                    // HenkiloID = henk.HenkiloID,
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
                HENKILOT dbItem = (from h in entities.HENKILOT
                                   where h.HenkiloID == henk.HenkiloID
                                   select h).FirstOrDefault();



                if (dbItem != null)
                {
                    dbItem.HenkiloID = henk.HenkiloID;
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

        public ActionResult delete(int id)
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