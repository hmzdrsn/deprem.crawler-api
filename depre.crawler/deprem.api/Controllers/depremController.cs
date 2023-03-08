using Microsoft.AspNetCore.Mvc;
using System.Text;
using HtmlAgilityPack;
using System.Data.SqlClient;
using deprem.api.Models;
using System.Runtime.Intrinsics.Arm;

namespace deprem.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class depremController : Controller
    {
        
        string kayitlisaat = "";
        string connectionString = "Data Source=wisgn103986\\MSSQLSERVER2;Initial Catalog= test;User Id=sa;Password=123456;";

        [HttpGet]
        public ActionResult<deprem.api.Models.deprem> sonDeprem()
        {
           
            deprem.api.Models.deprem dt = new Models.deprem();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM depremListesi ORDER BY id desc",conn);
                SqlDataReader reader = command.ExecuteReader();    
                while (reader.Read())
                {
                    dt.tarih = (string)reader["tarih"];
                    dt.saat = (string)reader["saat"];
                    dt.enlem = (string)reader["enlem"];
                    dt.boylam = (string)reader["boylam"];
                    dt.derinlik = (string)reader["derinlik"];
                    dt.md = (string)reader["md"];
                    dt.ml = (string)reader["ml"];
                    dt.mw = (string)reader["mw"];
                    dt.yer = (string)reader["yer"];
                    dt.cozumNiteliği = (string)reader["cozumNiteliği"];
                }
                reader.Close();
                conn.Close();
            }
            return Ok(dt);
        }

        [HttpGet]
        public ActionResult<deprem.api.Models.deprem> getAll()
        {
            List<deprem.api.Models.deprem> lst = new List<Models.deprem>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM depremListesi",conn);
                SqlDataReader reader = command.ExecuteReader();      
                while(reader.Read())
                {
                    deprem.api.Models.deprem dt = new Models.deprem();
                    dt.tarih= (string)reader["tarih"];
                    dt.saat = (string)reader["saat"];
                    dt.enlem= (string)reader["enlem"];
                    dt.boylam= (string)reader["boylam"];
                    dt.derinlik= (string)reader["derinlik"];
                    dt.md= (string)reader["md"];
                    dt.ml= (string)reader["ml"];
                    dt.mw= (string)reader["mw"];
                    dt.yer= (string)reader["yer"];
                    dt.cozumNiteliği= (string)reader["cozumNiteliği"];
                    lst.Add(dt);
                }
                reader.Close();
                conn.Close();
            }
            return Ok(lst);
        }
    }
}
