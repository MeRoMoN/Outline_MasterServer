using System;
using System.Collections.Generic;
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

        public int PostData(ServerData Data)
        {
            try
            {
                SqlConnection.Open();

                MySqlCommand Command = new MySqlCommand("AddServerEntry", SqlConnection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;

                Random Rndm = new Random();
                int ServerID = Rndm.Next(1, 200000000);

                Command.Parameters.AddWithValue("_ServerID", ServerID);
                Command.Parameters.AddWithValue("_IPAddress", Data.IPAddress);
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

    }
}