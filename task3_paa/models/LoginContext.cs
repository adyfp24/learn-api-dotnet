using System;
using System.Collections.Generic;
using task3_paa.Helpers;
using Npgsql;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace task3_paa.Models{
    public class LoginContext{
        private string __constr;
        private string __ErrorMsg;

        public LoginContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Login> Authentifikasi(string p_username, ring p_password, IConfiguration p_config)
        {
            List<Login> list1 = new List <Login>();
            string query = string.Format(@"SELECT ps.id_person , ps.nama, ps.alamat, pp.id_peran, p.nama_peran
            FROM users.person ps
            INNER JOIN users.peran_person pp ON ps.id_person=pp.id_peson
            INNER JOIN users.peran p ON pp.id_peran=p.id_peran
            where ps.username='{0}' and ps.password='{1}'; ", p_username, p_password);
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try{
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while(read.Read()){
                   list1.add(new Login()
                   {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString(), 
                        id_peran = reader["id_peran"].ToString(),
                        nama_peran = reader["nama_peran"].ToString(),
                        token = GenerateJwtToken(p_username, p_password, p_config)
                   });
                }
                cmd.Dispose();
                db.closeConnection();
            }catch(Exception){
                __ErrorMsg= ex.Message;
            }
            return list1;
        }
        private string GenerateJwtToken(string namaUser, string peran, IConfiguration pConfig)
        {
            var securityKey = new SymmetriSecurityKey(Encoding.ASCII.GetBytes(pConfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, securityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, namaUser),
                new Claim(ClaimTypes.Role, peran)
            };
            var token = new JwtSecurityToken(pConfig["Jwt:Issue"],
                pConfig["Jwt:Audience"]
                claims,
                expires: DataTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
