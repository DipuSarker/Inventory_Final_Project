using OST_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace OST_Inventory.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Dashboard()
        {
            if (Session["User"] != null)
            {
                List<BaseEquipment> plsData = BaseEquipment.ListEqipmentDate();
                ViewBag.plsData = plsData;

                DataTable dtCustEquip  = BaseCustomer.ListCustomerEquipment();
                ViewBag.dtCustEquip = dtCustEquip;

                ViewBag.txtName = "";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }


        [HttpPost]
        public ActionResult Dashboard(FormCollection frm, string btnSubmit)
        {

            if (btnSubmit == "Save Equipment")
            {
                BaseEquipment baseEquipment = new BaseEquipment();

                baseEquipment.Name = frm["ddlEquipment"].ToString();
                baseEquipment.EcCount = Convert.ToInt32(frm["txtQuantity"].ToString());
                baseEquipment.EntryDate = Convert.ToDateTime(frm["txtEntryDate"].ToString());

                int returnResult = baseEquipment.SaveEquipment();

                if (returnResult > 0)
                {
                    ViewBag.OerationResult = "Save Successfully";
                }
            }




            List<BaseEquipment> plsData = BaseEquipment.ListEqipmentDate();
            ViewBag.plsData = plsData;

            DataTable dtCustEquip = BaseCustomer.ListCustomerEquipment();
            ViewBag.dtCustEquip = dtCustEquip;

            ViewBag.txtName = "";
            if (btnSubmit == "Search")
            {
                ViewBag.txtName = frm["txtName"].ToString();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult LstEquipment()
        {
            List<BaseEquipment> plsData = BaseEquipment.ListEqipmentDate();
            var pdata = (from p in plsData
                         select
                         new
                         {
                             EquipmentId = p.EquipmentId,
                             Name = p.Name,
                             EcCount = p.EcCount.ToString(),
                             EntryDate = p.EntryDate.ToString()
                         });
            return Json(pdata, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult LstCustomer()
        {
            List<BaseCustomer> plsData = BaseCustomer.LstCustomerData();
            
            return Json(plsData, JsonRequestBehavior.AllowGet);
        }




    }
}