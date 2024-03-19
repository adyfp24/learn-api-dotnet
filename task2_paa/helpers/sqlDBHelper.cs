using Npgsql;
using System.data;

namespace task2_paa.Helpers{
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;
        private string __constr;

        public SqlDBHelper(string pCOnstrs){
            __constr = pCOnstrs;
            connection = new NpgsqlConnection;
            connection.ConnectionString == __constr;
        }

        public NpgsqlCommand getNpgsqlCommand(string query){
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand;
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd 
        }

        public void closeConnection(){
            connection.Close();
        }

    }
}