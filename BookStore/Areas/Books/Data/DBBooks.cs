using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using BookStore.Areas.Shelves.Data;
using System.Web.Mvc;

namespace BookStore.Areas.Books.Data
{
    public class DBBooks
    {

        public string connectionString = ConfigurationManager.ConnectionStrings["BookStoreEntities"].ConnectionString;

        public DataTable GetBooksFromDB()
        {
            DataTable datatable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_GetAllBooks", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(datatable);
                connection.Close();
            }

            return datatable;
        }


        public DataTable GetActiveShelvesFromDB()
        {
            DataTable datatable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_GetActiveShelves", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(datatable);
                connection.Close();
            }

            return datatable;
        }



        [HttpPost]
        public int InsertBooksToDB(mBooks mBooks)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertBook", connection);
                command.CommandType = CommandType.StoredProcedure; 
                command.Parameters.Add("@BookCode", SqlDbType.VarChar, 20).Value = mBooks.BookCode; 
                command.Parameters.Add("@BookName", SqlDbType.VarChar, 100).Value = mBooks.BookName;
                command.Parameters.Add("@BookAuthor", SqlDbType.VarChar, 20).Value = mBooks.BookAuthor; 
                command.Parameters.Add("@BookIsAvail", SqlDbType.VarChar, 20).Value = mBooks.BookIsAvail; 
                command.Parameters.Add("@BookPrice", SqlDbType.Money).Value = mBooks.BookPrice;
                command.Parameters.Add("@BookselfId", SqlDbType.Int).Value = mBooks.BookselfId; 

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }
        [HttpPost]
        public int UpdateBooksToDB(mBooks mBooks)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_UpdateBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@BookCode", SqlDbType.VarChar, 20).Value = mBooks.BookCode;
                command.Parameters.Add("@BookName", SqlDbType.VarChar, 100).Value = mBooks.BookName;
                command.Parameters.Add("@BookAuthor", SqlDbType.VarChar, 20).Value = mBooks.BookAuthor;
                command.Parameters.Add("@BookIsAvail", SqlDbType.VarChar, 20).Value = mBooks.BookIsAvail;
                command.Parameters.Add("@BookPrice", SqlDbType.Money).Value = mBooks.BookPrice;
                command.Parameters.Add("@BookselfId", SqlDbType.Int).Value = mBooks.BookselfId;
                command.Parameters.Add("@BookId", SqlDbType.Int).Value = mBooks.BookId;

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }


        [HttpPost]
        public int DeleteBooksToDB(mBooks mbooks)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_DeleteBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@BookId", SqlDbType.Int).Value = mbooks.BookId;
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }

    }


}