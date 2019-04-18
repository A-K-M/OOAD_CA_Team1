using OOAD_CA_Team1.DAO;
using OOAD_CA_Team1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OOAD_CA_Team1.Controllers
{
    public class EnquiryTourLeadCostController : Controller
    {
        // GET: EnquiryTourLeadCost
        public ActionResult Index()
        {
            ModelState.Clear();
            var vm = new EnquiryTourLeadViewModel();
            ConfigureViewModel(vm);
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(EnquiryTourLeadViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ConfigureViewModel(vm);
                return View(vm);
            }

            var cost = CostCalculator.CalculateTourLeadCost(vm.SelectedTourLead, Convert.ToInt32(vm.NoOfDays));

            ConfigureViewModel(vm);
            ViewBag.Cost = cost;
            return View(vm);
        }

        private void ConfigureViewModel(EnquiryTourLeadViewModel vm)
        {
            var repo = new EnquiryTourLeadCostRepository();
            IEnumerable<TourLeader> tourLeads = repo.GetTourLeads();
            vm.TourLeads = new SelectList(tourLeads, "TourleaderId", "Name");
        }
    }
}