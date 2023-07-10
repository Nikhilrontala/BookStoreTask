using BookStore.Areas.Books.Data;
using BookStore.Areas.Racks.Data;
using BookStore.Areas.Shelves.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace BookStore.Areas.Books.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books/Books



        // GET: Books/Books
        public ActionResult Books()
        {
            return View();

        }
        public ActionResult BooksCheck(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = "vylfAou9cDlGAdTF4IolUONGLNPYtfWTgkdU8OqfAH0=";
                try
                {
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    SecurityToken validatedToken;
                    var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                    var claims = principal.Claims;
                    var username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    return View();
                }
                catch (SecurityTokenException ex)
                {
                    throw;
                }
            }
            else
            {
                return View() ;
            }
        }




        public string GenerateSecretKey(int keyLength)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[keyLength / 8]; // Convert bits to bytes
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
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

        public JsonResult InsertBooks(mBooks books)
        {
            DBBooks dBBooks = new DBBooks();
            int rowsAffected = dBBooks.InsertBooksToDB(books);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Book Inserted successfully", rowsAffected });
            }
            if (rowsAffected == -1)
            {
                return Json(new { success = true, message = "Book Details Already exists", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Failed to Insert Book", rowsAffected });
            }
        }


        public ActionResult EditBooks(mBooks books)
        {
            return View(books);
        }
        public ActionResult ViewBooks(mBooks books)
        {
            return View(books);
        }


        public JsonResult UpdateBook(mBooks mBooks)
        {
            DBBooks dBbooks = new DBBooks();
            int rowsAffected = dBbooks.UpdateBooksToDB(mBooks);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Book Updated successfully", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Same Book Code exists", rowsAffected });
            }
        }

        public JsonResult DeleteBooks(mBooks books)
        {
            DBBooks dBBooks = new DBBooks();
            int rowsAffected = dBBooks.DeleteBooksToDB(books);
            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Book Deleted successfully", rowsAffected });
            }
            else
            {
                return Json(new { success = false, message = "Error Occured While Deleting Book Code", rowsAffected });
            }
        }

    }
}