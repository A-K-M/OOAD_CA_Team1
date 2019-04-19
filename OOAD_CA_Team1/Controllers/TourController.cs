using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOAD_CA_Team1.Models;
using OOAD_CA_Team1.DAO;
using System.Diagnostics;

namespace OOAD_CA_Team1.Controllers
{
    public class TourController : Controller
    {
        // GET: Tour
        public ActionResult TourList()
        {
            List<Tours> tourlist = new List<Tours>();
            tourlist = TourDAO.GetTourList();
            return View(tourlist);
        }
        public ActionResult AssignTourLeader(int id)
        {

            Tours tourinfo = new Tours();
            tourinfo = TourDAO.GetTourInfoById(id);

            List<Tourleaders> tl_list = new List<Tourleaders>();
            tl_list = TourDAO.GetTourLeaderById();

            ViewBag.tl_list = tl_list;
            ViewBag.tid = id;

            return View(tourinfo);
        }

        public ActionResult AssignLeader(int tid)
        {
            int tl_id;
            foreach (string key in Request.Form.AllKeys)
            {
                //Debug.WriteLine("Keys : : : " + key);
                //Debug.WriteLine("Value : : : " + Request[key]);
                var test = Request[key];
                if (key == "TourLeader" && Request[key] != "")
                {
                    tl_id = Convert.ToInt32(Request[key]);
                    TourDAO.AssignTourleader(tid, tl_id);
                }
            }
                return RedirectToAction("TourList");
        }
       
    }
}