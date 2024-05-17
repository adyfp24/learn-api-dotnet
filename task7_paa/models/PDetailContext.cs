using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using task7_paa.Helpers;

namespace task7_paa.Models
{
    public class PDetailContext
    {
        private string __constr;
        private string __ErrorMsg;
        public PDetailContext(string pConstr)
        {
            __constr = pConstr;
        }

        public void InsertPersonDetails(List<PersonDetail> personDetails)
        {
            string query = string.Format(@"INSERT INTO person_detail (id, name, saldo, hutang) VALUES (@id, @name, @saldo, @hutang);");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try{
                foreach (var person in personDetails)
                {
                    NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                    cmd.Parameters.AddWithValue("id", person.id);
                    cmd.Parameters.AddWithValue("name", person.name);
                    cmd.Parameters.AddWithValue("saldo", person.saldo);
                    cmd.Parameters.AddWithValue("hutang", person.hutang);
                    cmd.ExecuteNonQuery();       
                    cmd.Dispose();
                }
                
            }catch(Exception ex){
                __ErrorMsg = ex.Message;
            }
             finally
            {
                db.closeConnection(); 
            }
        }

        
    }
}
