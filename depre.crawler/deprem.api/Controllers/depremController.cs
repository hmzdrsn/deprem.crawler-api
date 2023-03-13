using Microsoft.AspNetCore.Mvc;
using System.Text;
using HtmlAgilityPack;
using System.Data.SqlClient;
using deprem.Model.Models;
using System.Runtime.Intrinsics.Arm;
using deprem.Database.Data;
using Microsoft.IdentityModel.Tokens;
//using deprem.Database.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Microsoft.Extensions.Options;


namespace deprem.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class depremController : Controller
    {
        

        [HttpGet]
        public ActionResult<deprem.Model.Models.deprem> sonDeprem()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=wisgn103986\\MSSQLSERVER2;Database= test2;User Id=sa;Password=123456;TrustServerCertificate=True");

            deprem.Model.Models.deprem deprem = new Model.Models.deprem();

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                deprem = context.Depremler.OrderByDescending(r => r.Id).FirstOrDefault();
            }
            return Ok(deprem);
        }

        [HttpGet]
        public ActionResult<deprem.Model.Models.deprem> getAll()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=wisgn103986\\MSSQLSERVER2;Database= test2;User Id=sa;Password=123456;TrustServerCertificate=True");

            using(var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                var tumKayitlar = context.Depremler.ToList();
                return Ok(tumKayitlar);
            }
           
        }


       
    }
}
