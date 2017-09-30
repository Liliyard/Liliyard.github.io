using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }  // 列表ID
        public int UserID { get; set; }    // 用户ID
        public int GenreID { get; set; }  // 类型
        public int ClassesID { get; set; }  // 分类
        public decimal Money { get; set; }  // 金额
        public DateTime Date { get; set; }  // 日期
        public String Remark { get; set; }  // 备注
    }
}