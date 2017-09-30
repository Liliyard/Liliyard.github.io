using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Contention
    {
        [Key]
        public int ContentionID { get; set; }
        public int TitleID { get; set; }
        public int UserID { get; set; }
        public String Contentions { get; set; }
        public DateTime CDate { get; set; }
        public Boolean CIsFirstFloor { get; set; }
    }
}