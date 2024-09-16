using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace OutlineMasterServer.Models
{
    public class DatabaseInterface
    {
        private MySqlConnection SqlConnection;

        public DatabaseInterface()
        {
            string ConnectionString = "server=127.0.0.1;uid=MeRoMoN;pwd=mers1Q2l3;database=outline_data";
            SqlConnection = new MySqlConnection(ConnectionString);
        }

        public string GetUserIPAddress()
        {
            string IP = System.Web.HttpContext.Current.Request.UserHostAddress;
            if (IP == "::1")
                IP = "127.0.0.1";
            return IP;
        }

        public int PostData(ServerData Data)
        {
            try
            {
                SqlConnection.Open();

                //Checking for existing servers from current clients IP address
                MySqlCommand Command = new MySqlCommand("DeleteServerEntry", SqlConnection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("_IPAddress", GetUserIPAddress());

                Command.ExecuteNonQuery();



                // Adding a new server
                Command = new MySqlCommand("AddServerEntry", SqlConnection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;

                Random Rndm = new Random();
                int ServerID = Rndm.Next(1, 200000000);

                Command.Parameters.AddWithValue("_ServerID", ServerID);
                Command.Parameters.AddWithValue("_IPAddress", GetUserIPAddress());
                Command.Parameters.AddWithValue("_ServerName", Data.ServerName);
                Command.Parameters.AddWithValue("_MapName", Data.MapName);
                Command.Parameters.AddWithValue("_CurrentPlayers", Data.CurrentPlayers);
                Command.Parameters.AddWithValue("_MaxPlayers", Data.MaxPlayers);

                Command.ExecuteNonQuery();

                SqlConnection.Close();
                return ServerID;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public void DeleteData()
        {
            try
            {
                SqlConnection.Open();

                MySqlCommand Command = new MySqlCommand("DeleteServerEntry", SqlConnection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("_IPAddress", GetUserIPAddress());

                Command.ExecuteNonQuery();

                SqlConnection.Close();
            }
            catch (Exception e)
            {

            }
        }

        public DataTable GetAllServers()
        {
            try
            {
                SqlConnection.Open();

                MySqlCommand Command = new MySqlCommand("GetAllServerEntries", SqlConnection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;

                DataTable dataTable = new DataTable();
                dataTable.Load(Command.ExecuteReader());

                Command.ExecuteNonQuery();

                SqlConnection.Close();
                return dataTable;
            }
            catch (Exception e)
            {
                return new DataTable();
            }
        }

    }
}