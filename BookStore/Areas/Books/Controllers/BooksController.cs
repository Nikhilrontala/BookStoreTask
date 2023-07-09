using BookStore.Areas.Books.Data;
using BookStore.Areas.Racks.Data;
using BookStore.Areas.Shelves.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Books.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books/Books
        public ActionResult Books()
        {
            return View();
        }

        public JsonResult GetAllBooks()
        {
            DBBooks dbBooks = new DBBooks();
            DataTable dataTable = dbBooks.GetBooksFromDB();

            List<mBooks> Books = new List<mBooks>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    mBooks book = new mBooks();
                    book.BookId = Convert.ToInt32(row["BookId"]);
                    book.selfCode =row["selfCode"].ToString();
                    book.selfStatus = row["selfStatus"].ToString();
                    book.rackCode = row["rackCode"].ToString();
                    book.BookCode = row["BookCode"].ToString();
                    book.BookName = row["BookName"].ToString();
                    book.BookAuthor = row["BookAuthor"].ToString();
                    book.BookIsAvail = row["BookIsAvail"].ToString();
                    book.BookStatus = row["BookStatus"].ToString();
                    book.BookPrice = decimal.Parse(row["BookPrice"].ToString());
                    book.BookselfId = Convert.ToInt32(row["BookselfId"].ToString());
                    Books.Add(book);
                }
            }
            return Json(Books, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddBook()
        {
            return View();
        }


        public JsonResult GetActiveShelves()
        {
            DBBooks dBBooks = new DBBooks();
            DataTable dataTable = dBBooks.GetActiveShelvesFromDB();

            List<mShelves> shelves = new List<mShelves>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    mShelves shelf = new mShelves();
                    shelf.selfId = Convert.ToInt32(row["selfId"]);
                    shelf.selfCode = row["selfCode"].ToString();
                    shelves.Add(shelf);
                }
            }
            return Json(shelves, JsonRequestBehavior.AllowGet);


        }

    }
}