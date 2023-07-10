using BookStore.Areas.Racks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Shelves.Data
{
    public class DBShelves
    {

        public string connectionString = ConfigurationManager.ConnectionStrings["BookStoreEntities"].ConnectionString;

        public DataTable GetShelvesFromDB()
        {
            DataTable datatable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_GetAllBS_Shelves", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(datatable);
                connection.Close();
            }

            return datatable;
        }


        public DataTable GetActiveRacksFromDB()
        {
            DataTable datatable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_GetACtiveRacks", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(datatable);
                connection.Close();
            }

            return datatable;
        }


        [HttpPost]
        public int InsertShelfToDB(mShelves mShelves)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_InsertShelf", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@selfRackId", SqlDbType.Int).Value = mShelves.selfRackId;
                command.Parameters.Add("@selfCode", SqlDbType.VarChar, 20).Value = mShelves.selfCode;
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }

        [HttpPost]
        public int UpdateShelvesToDB(mShelves mShelves)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_UpdateShelf", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@selfId", SqlDbType.Int).Value = mShelves.selfId;
                command.Parameters.Add("@selfCode", SqlDbType.VarChar, 20).Value = mShelves.selfCode;
                command.Parameters.Add("@selfRackId", SqlDbType.Int).Value = mShelves.selfRackId;

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }

        [HttpPost]
        public int DeleteShelvesToDB(mShelves mShelves)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_DeleteShelves", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@selfId", SqlDbType.Int).Value = mShelves.selfId;
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }

    }
}