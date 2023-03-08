using HtmlAgilityPack;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

var url = "http://www.koeri.boun.edu.tr/scripts/lst0.asp";
var xPath = @"//html/body/pre";
string kayitliSaat="";
string connectionString = "Data Source=wisgn103986\\MSSQLSERVER2;Initial Catalog= test;User Id=sa;Password=123456;";
string tarih, saat, enlem, boylam, derinlik, md, ml, mw, yer, cozumNiteligi;
using (SqlConnection conn = new SqlConnection(connectionString))
{
    try 
    { 
    conn.Open();
    Console.WriteLine("Bağlantı Başarılı.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Bağlantı Hatası : "+ex);
    }
}

while (true)
{
    var web = new HtmlWeb();
    web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3";
    web.OverrideEncoding = Encoding.UTF8;
    var doc = web.Load(url);
    HtmlNodeCollection data = doc.DocumentNode.SelectNodes(xPath);
    foreach (HtmlNode item in data)
    {
        
        string veri = item.InnerText;
        tarih = veri.Substring(586, 10).Trim();
        saat = veri.Substring(597, 8).Trim();//17:17:33
        enlem = veri.Substring(607, 8).Trim();
        boylam = veri.Substring(617, 7).Trim();
        derinlik = veri.Substring(631, 4).Trim();
        md = veri.Substring(641, 3).Trim();
        ml = veri.Substring(646, 3).Trim();
        mw = veri.Substring(651, 3).Trim();
        yer = veri.Substring(657, 50).Trim();
        cozumNiteligi = veri.Substring(707, 6).Trim();
        if (cozumNiteligi.Substring(0, 1) == "�");
        {
            cozumNiteligi = "İlksel";
        }

        CompareandInsert(saat);
    }
    Thread.Sleep(TimeSpan.FromSeconds(10));
}


void Insert(string tarih, string saat, string enlem, string boylam, string derinlik, string md, string ml, string mw, string yer, string cozumNiteligi)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        SqlCommand command = new SqlCommand("INSERT INTO depremListesi (tarih,saat,enlem,boylam,derinlik,md,ml,mw,yer,cozumNiteliği)" +
            "VALUES ('" + tarih + "','"+saat+ "', '"+enlem+ "','"+boylam+ "','"+derinlik+"','"+md+"','"+ml+"','"+mw+"','"+yer+"','"+cozumNiteligi+"')",connection);
        int kayitSayisi =command.ExecuteNonQuery();
        Console.WriteLine("{0} kayıt eklendi",kayitSayisi);
        connection.Close();
    }
}



void CompareandInsert(string s1)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM depremListesi ORDER BY id desc", connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            kayitliSaat = (string)reader["saat"];
        }
        reader.Close();
    }
    if (kayitliSaat == s1)
    {

    }
    else
    {
        Insert(tarih, saat, enlem, boylam, derinlik, md, ml, mw, yer, cozumNiteligi);
    }
}