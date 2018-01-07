using MvcTietokantaAzure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTietokantaAzure.Controllers
{
    public class ProjektitController : Controller
    {
        // GET: Projektit
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            KoulukantaEntities entities = new KoulukantaEntities();

            var model = (from p in entities.PROJEKTIT
                         select new
                         {
                             ProjektiID = p.ProjektiID,
                             Nimi = p.Nimi,
                         }).ToList();


            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            Response.Expires = -1;
            Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSingleProjektit(int id)
        {
            KoulukantaEntities entities = new KoulukantaEntities();

            var model = (from p in entities.PROJEKTIT
                         where p.ProjektiID == id
                         select new
                         {
                             ProjektiID = p.ProjektiID,
                             Nimi = p.Nimi
                         }).FirstOrDefault();


            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult update(PROJEKTIT pro)
        {

            KoulukantaEntities entities = new KoulukantaEntities();

            bool OK = false;

            // onko kyseessä muokkaus vai uuden lisääminen?
            if (pro.ProjektiID == 0)
            {
                // kyseessä on uuden asiakkaan lisääminen, kopioidaan kentät
                PROJEKTIT dbItem = new PROJEKTIT()
                {

                    ProjektiID = pro.ProjektiID,
                    Nimi = pro.Nimi
                };

                // tallennus tietokantaan
                entities.PROJEKTIT.Add(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            else
            {
                // muokkaus, haetaan id:n perusteella riviä tietokannasta
                PROJEKTIT dbItem = (from p in entities.PROJEKTIT
                                 where p.ProjektiID== pro.ProjektiID
                                 select p).FirstOrDefault();



                if (dbItem != null)
                {
                    
                    dbItem.ProjektiID = pro.ProjektiID;
                    dbItem.Nimi = pro.Nimi;
                   


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
           PROJEKTIT dbItem = (from p in entities.PROJEKTIT
                             where p.ProjektiID == id
                             select p).FirstOrDefault();
            if (dbItem != null)
            {
                // tietokannasta poisto
                entities.PROJEKTIT.Remove(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            entities.Dispose();

            return Json(OK, JsonRequestBehavior.AllowGet);
        }

    }
}