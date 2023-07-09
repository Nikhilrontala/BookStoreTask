using BookStore.Areas.Racks.Data;
using BookStore.Areas.Shelves.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Shelves.Controllers
{
    public class ShelvesController : Controller
    {
        // GET: Shelves/Shelves
        public ActionResult Shelves()
        {
            return View();
        }

        public JsonResult GetAllShelves()
        {
            DBShelves dbShelves = new DBShelves();
            DataTable dataTable = dbShelves.GetShelvesFromDB();

            List<mShelves> shelves = new List<mShelves>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    mShelves shelf = new mShelves();
                    shelf.selfId = Convert.ToInt32(row["selfId"]);
                    shelf.selfCode = row["selfCode"].ToString();
                    shelf.selfRackId = Convert.ToInt32(row["selfRackId"].ToString());
                    shelf.selfStatus = row["selfStatus"].ToString();
                    shelf.rackStatus = row["rackStatus"].ToString();
                    shelf.rackCode = row["rackCode"].ToString();

                    shelves.Add(shelf);
                }
            }
            return Json(shelves, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddShelf()
        {
            return View();
        }

        public JsonResult GetActiveShelves()
        {
            DBShelves dbRacks = new DBShelves();
            DataTable dataTable = dbRacks.GetActiveRacksFromDB();

            List<mRacks> racks = new List<mRacks>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    mRacks rack = new mRacks();
                    rack.rackId = Convert.ToInt32(row["rackId"]);
                    rack.rackCode = row["rackCode"].ToString();
                    racks.Add(rack);
                }
            }
            return Json(racks, JsonRequestBehavior.AllowGet);


        }


        public JsonResult InsertShelves(mShelves mShelves)
        {
            DBShelves dBShelves = new DBShelves();
            int rowsAffected = dBShelves.InsertShelfToDB(mShelves);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Shelf Inserted successfully", rowsAffected });
            }
            if (rowsAffected == -1)
            {
                return Json(new { success = true, message = "Shelf Details Already exists", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Failed to Insert Shelf", rowsAffected });
            }
        }



        public ActionResult EditShelves(mShelves mShelves)
        {
            return View(mShelves);
        }


        public JsonResult UpdateShelves(mShelves mShelves)
        {
            DBShelves dBShelves = new DBShelves();
            int rowsAffected = dBShelves.UpdateShelvesToDB(mShelves);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Shelf Updated successfully", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Same Shelf Code exists", rowsAffected });
            }
        }


        public JsonResult DeleteShelves(mShelves mshelves)
        {
            DBShelves dBShelves = new DBShelves();
            int rowsAffected = dBShelves.DeleteShelvesToDB(mshelves);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Shelf Deleted successfully", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Error Occured While Deleting Shelf Code", rowsAffected });
            }
        }

    }
}