using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        public String Gname { get; set; }
    }
}