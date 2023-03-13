using HtmlAgilityPack;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.Extensions.DependencyInjection;
using deprem.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using deprem.Model.Models;


var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlServer("Server=wisgn103986\\MSSQLSERVER2;Database= test2;User Id=sa;Password=123456;TrustServerCertificate=True");

var url = "http://www.koeri.boun.edu.tr/scripts/lst0.asp";
var xPath = @"//html/body/pre";

while (true)
{
    var web = new HtmlWeb();
    web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3";
    web.OverrideEncoding = Encoding.UTF8;
    var doc = web.Load(url);
    HtmlNodeCollection data = doc.DocumentNode.SelectNodes(xPath);
    deprem.Model.Models.deprem deprem = new deprem.Model.Models.deprem();
    foreach (HtmlNode item in data)
    {
        
        string veri = item.InnerText;
        deprem.tarih = veri.Substring(586, 10).Trim();
        deprem.saat = veri.Substring(597, 8).Trim();//17:17:33
        deprem.enlem = veri.Substring(607, 8).Trim();
        deprem.boylam = veri.Substring(617, 7).Trim();
        deprem.derinlik = veri.Substring(631, 4).Trim();
        deprem.md = veri.Substring(641, 3).Trim();
        deprem.ml = veri.Substring(646, 3).Trim();
        deprem.mw = veri.Substring(651, 3).Trim();
        deprem.yer = veri.Substring(657, 50).Trim();
        deprem.cozumNiteliği = veri.Substring(707, 6).Trim();
        if (deprem.cozumNiteliği.Substring(0, 1) == "�");
        {
            deprem.cozumNiteliği = "İlksel";
        }
   
        CompareandInsert(deprem);
    }
    Thread.Sleep(TimeSpan.FromSeconds(60));//**
}




void Insert(deprem.Model.Models.deprem deprem)
{
    using(var context = new ApplicationDbContext(optionsBuilder.Options))
    {
        context.Depremler.Add(deprem);
        context.SaveChanges();
        Console.WriteLine("Kayıt Eklendi");
    }
}

void CompareandInsert(deprem.Model.Models.deprem deprem)
{
    using (var context = new ApplicationDbContext(optionsBuilder.Options))
    {
        var sonkayit =context.Depremler.OrderByDescending(r => r.Id).FirstOrDefault();
        if(sonkayit?.saat == deprem.saat)
        {

        }
        else
        {
            Insert(deprem);
        }
    }
}
