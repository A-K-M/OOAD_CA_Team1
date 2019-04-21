using OOAD_CA_Team1.TourReservationSysDB;
using OOAD_CA_Team1.Models;
using OOAD_CA_Team1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OOAD_CA_Team1.Controllers
{
    public class TourLeadController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Cost");
        }
        public ActionResult Cost()
        {
            ModelState.Clear();
            var vm = new TourLeadCostViewModel();
            ConfigureViewModel(vm);
            return View(vm);
        }
        [HttpPost]
        public ActionResult Cost(TourLeadCostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ConfigureViewModel(vm);
                return View(vm);
            }
            double cost = 0.0;
            try
            {
                cost = CostCalculator.CalculateTourLeadCost(vm.SelectedTourLead, Convert.ToInt32(vm.NoOfDays));
            }
            catch (Exception)
            {
                TempData["msg"] = "<script>alert('Sorry! Fail to calculate cost. Please try again.');</script>";
            }
            ConfigureViewModel(vm);
            ViewBag.Cost = cost;
            return View(vm);
        }

        private void ConfigureViewModel(TourLeadCostViewModel vm)
        {
            var repo = new DBTourLeader();
            IEnumerable<TourLeader> tourLeads = repo.GetTourLeads();
            foreach(var lead in tourLeads)
            {
                var idAndName = $"{lead.Name} (ID: {lead.TourleaderId})";
                lead.Name = idAndName;
            }
            vm.TourLeads = new SelectList(tourLeads, "TourleaderId", "Name");
        }
    }
}