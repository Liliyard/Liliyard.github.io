using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class AccountViewModel
    {
        public int AccountID { get; set; }  // 列表ID
        public int UserID { get; set; }    // 用户ID
        public String Genre { get; set; }  // 类型
        public String Classes { get; set; }  // 分类
        public String Money { get; set; }  // 金额
        public String Date { get; set; }  // 日期
        public String Remark { get; set; }  // 备注
        public String TypeColor { get; set; }   // 分类颜色，收入为绿，支出为红
    }
}