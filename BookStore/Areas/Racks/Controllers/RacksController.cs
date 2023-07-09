using Antlr.Runtime.Misc;
using BookStore.Areas.Racks.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace BookStore.Areas.Racks.Controllers
{
    public class RacksController : Controller
    {
        // GET: Racks/Racks
        public ActionResult Racks()
        {
            return View();
        }

        public JsonResult GetAllRacks()
        {
            DBRacks dbRacks = new DBRacks();
            DataTable dataTable = dbRacks.GetRacksFromDB();

            List<mRacks> racks = new List<mRacks>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    mRacks rack = new mRacks();
                    rack.rackId = Convert.ToInt32(row["rackId"]);
                    rack.rackCode = row["rackCode"].ToString();
                    rack.rackStatus = row["rackStatus"].ToString();
                    racks.Add(rack);
                }
            }
            return Json(racks, JsonRequestBehavior.AllowGet);
        

        }

        public ActionResult AddRack()
        {
            return View();
        }


        public JsonResult InsertRack(mRacks mRacks)
        {
            DBRacks dbRacks = new DBRacks();
            int rowsAffected = dbRacks.InsertRacksToDB(mRacks);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Rack Inserted successfully", rowsAffected });
            }
            if (rowsAffected == -1)
            {
                return Json(new { success = true, message = "Rack Details Already exists", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Failed to Insert rack", rowsAffected });
            }
        }


        public ActionResult EditRack(mRacks mRacks)
        {
            return View(mRacks);
        }

        public JsonResult UpdateRack(mRacks mRacks)
        {
            DBRacks dbRacks = new DBRacks();
            int rowsAffected = dbRacks.UpdateRacksToDB(mRacks);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Rack Updated successfully", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Same Rack Code exists", rowsAffected });
            }
        }

        public JsonResult DeleteRack(mRacks mRacks)
        {
            DBRacks dbRacks = new DBRacks();
            int rowsAffected = dbRacks.DeleteRacksToDB(mRacks);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Rack Deleted successfully", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Error Occured While Deleting Rack Code", rowsAffected });
            }
        }

    }
}