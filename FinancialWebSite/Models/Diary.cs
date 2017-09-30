using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Diary
    {
        [Key]
        public int DiaryID { get; set; }
        public int AdminID { get; set; }
        public int ApplyID { get; set; }
        public DateTime DiaryDate { get; set; }
        public String ApplyState { get; set; }
    }
}