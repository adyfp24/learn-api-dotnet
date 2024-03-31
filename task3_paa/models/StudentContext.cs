using System;
using System.Collections.Generic;
using task3_paa.Helpers;
using Npgsql;

namespace task3_paa.Models
{
    public class StudentContext
    {
        private string __constr;
        private string __ErrorMsg;
        
        public StudentContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Student> ListStudent()
        {
            List<Student> list1 = new List<Student>();
            string query = string.Format(@"SELECT id_murid, nama, alamat, nisn FROM sekolah.murid");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Student()
                    {
                        id_murid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        nisn = reader["nisn"].ToString()
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

        public Student getStudentById(int id_murid)
        {
            string query = "SELECT id_murid, nama, alamat, nisn FROM sekolah.murid WHERE id_murid = @id_murid";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try{
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_murid", id_murid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return new Student()
                    {
                        id_murid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        nisn = reader["nisn"].ToString()
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
        public void addStudent(Student student)
        {
            string query = "INSERT INTO sekolah.murid(nama, alamat, nisn) VALUES(@nama, @alamat, @nisn)";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try{
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", student.nama);
                cmd.Parameters.AddWithValue("@alamat", student.alamat);
                cmd.Parameters.AddWithValue("@nisn", student.nisn);
                cmd.ExecuteNonQuery();      
            } catch (Exception ex){
                __ErrorMsg = ex.Message;
            }
        }
        public void updateStudent(int id_murid, Student student)
        {
            string query = "UPDATE sekolah.murid SET nama = @nama, alamat = @alamat, nisn = @nisn WHERE id_murid = @id_murid";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try{
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", student.nama);
                cmd.Parameters.AddWithValue("@alamat", student.alamat);
                cmd.Parameters.AddWithValue("@nisn", student.nisn);
                cmd.Parameters.AddWithValue("@id_murid", id_murid);
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                __ErrorMsg = ex.Message;
            }
        }

        public void deleteStudent(int id_murid)
        {
            string query = "DELETE FROM sekolah.murid WHERE id_murid = @id_murid";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try{
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_murid", id_murid);
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                __ErrorMsg = ex.Message;
            }
        }
    }
}
