using System;
using System.Collections.Generic;
using task2_paa.Helpers;
using Npgsql;

namespace task2_paa.Models
{
    public class PersonContext
    {
        private string __constr;
        private string __ErrorMsg;
        
        public PersonContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();
            string query = string.Format(@"SELECT id_person, nama, alamat, email FROM users.Person");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Person()
                    {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString()
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            return list1;
        }

        public Person getPersonById(int id_person)
        {
            string query = "SELECT id_person, nama, alamat, email FROM users.Person WHERE id_person = @id_person";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try{
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_person", id_person);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return new Person()
                    {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString()
                    };
                }
                cmd.Dispose();
                db.closeConnection();
                return null;
            }catch (Exception ex) {
                __ErrorMsg = ex.Message;
                return null;
            }
        }
        public void addPerson(Person person)
        {
            string query = "INSERT INTO users.Person(nama, alamat, email) VALUES(@nama, @alamat, @email)";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try{
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", person.nama);
                cmd.Parameters.AddWithValue("@alamat", person.alamat);
                cmd.Parameters.AddWithValue("@email", person.email);
                cmd.ExecuteNonQuery();      
            } catch (Exception ex){
                __ErrorMsg = ex.Message;
            }
        }
        public void updatePerson(int id_person, Person person)
        {
            string query = "UPDATE users.Person SET nama = @nama, alamat = @alamat, email = @email WHERE id_person = @id_person";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try{
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", person.nama);
                cmd.Parameters.AddWithValue("@alamat", person.alamat);
                cmd.Parameters.AddWithValue("@email", person.email);
                cmd.Parameters.AddWithValue("@id_person", person.id_person);
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                __ErrorMsg = ex.Message;
            }
        }
    }
}
