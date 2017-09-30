using FinancialWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class BorrowViewModel : BaseViewModel
    {
        public int BorrowID { get; set; }   // 墙ID
        public int UserID { get; set; }     // 用户ID
        public String BTitle { get; set; }   // 借贷标题
        public String BTotal { get; set; }     // 总贷款金额
        public String BMonthShouldPay { get; set; }  // 每月应还贷款
        public String BNextRepay { get; set; }   // 下个还款日
        public int BShouldPay { get; set; }     // 分期数
        public int BHavePay { get; set; }   // 已还期数
        public String BRemark { get; set; }  // 备注
        public String Bcolor { get; set; }  // 背景颜色
    }
}