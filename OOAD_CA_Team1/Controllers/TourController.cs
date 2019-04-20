﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOAD_CA_Team1.Models;
using OOAD_CA_Team1.TourReservationSysDB;
using System.Diagnostics;

namespace OOAD_CA_Team1.Controllers
{
    public class TourController : Controller
    {
        // GET: Tour
        public ActionResult TourList()
        {
            List<Tour> tourlist = new List<Tour>();
            tourlist = TourRepository.GetTourList();
            return View(tourlist);
        }
        public ActionResult AssignTourLeader(int tid)
        {
            Tour tourinfo = TourRepository.GetTourDetailsById(tid);
            List<TourLeader> leader_list = new List<TourLeader>();
            leader_list = EnquiryTourLeadCostRepository.GetAvailableTourLeaders(tid, tourinfo.TourPackageId);

            ViewBag.tl_list = leader_list;
            ViewBag.tid = tid;

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
                    EnquiryTourLeadCostRepository.AssignTourleader(tid, tl_id);
                }
            }
                return RedirectToAction("TourList");
        }
       
    }
}