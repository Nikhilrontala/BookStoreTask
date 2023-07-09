using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Racks.Data
{
    public class DBRacks
    {

        public string connectionString = ConfigurationManager.ConnectionStrings["BookStoreEntities"].ConnectionString;

        public DataTable GetRacksFromDB()
        {
            DataTable datatable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_GetAllRacks", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(datatable);
                connection.Close();
            }

            return datatable;
        }

        [HttpPost]
        public int InsertRacksToDB(mRacks racks)
        {
            int rowsAffected = 0; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_InsertRack", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@rackCode", SqlDbType.VarChar, 20).Value = racks.rackCode;
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }

        [HttpPost]
        public int UpdateRacksToDB(mRacks racks)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_UpdateRack", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@rackId", SqlDbType.Int).Value = racks.rackId;
                command.Parameters.Add("@newRackCode", SqlDbType.VarChar, 20).Value = racks.rackCode;

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }
        [HttpPost]
        public int DeleteRacksToDB(mRacks racks)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_DeleteRack", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@rackId", SqlDbType.Int).Value = racks.rackId;
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            return rowsAffected;
        }
    }
}