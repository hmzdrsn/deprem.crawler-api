using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprem.Model.Models
{
    public class deprem
    {
        public int Id { get; set; }
        public string? tarih { get; set; }

        public string? saat { get; set; }

        public string? enlem { get; set; }
        public string? boylam { get; set; }
        public string? derinlik { get; set; }
        public string? md { get; set; }
        public string? ml { get; set; }
        public string? mw { get; set; }
        public string? yer { get; set; }
        public string? cozumNiteliği { get; set; }
    }
}
