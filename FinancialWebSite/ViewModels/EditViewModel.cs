using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class EditViewModel : BaseViewModel
    {
        public int AccountID { get; set; }  // 列表ID
        public int UserID { get; set; }    // 用户ID
        public int GenreID { get; set; }  // 类型
        public int ClassesID { get; set; }  // 分类
        public decimal Money { get; set; }  // 金额
        public String Date { get; set; }  // 日期
        public String Remark { get; set; }  // 备注
        public int ClassIndex { get; set; }  // 下标
    }
}