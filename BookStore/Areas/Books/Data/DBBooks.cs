using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

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
    }
}