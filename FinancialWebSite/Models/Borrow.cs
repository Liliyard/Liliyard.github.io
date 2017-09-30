using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Borrow
    {
        [Key]
        public int BorrowID { get; set; }   // 墙ID
        public int UserID { get; set; }     // 用户ID
        public String BTitle { get; set; }   // 借贷标题
        public decimal BTotal { get; set; }     // 总贷款金额
        public decimal BMonthShouldPay { get; set; }  // 每月应还贷款
        public DateTime BNextRepay { get; set; }   // 下个还款日
        public int BShouldPay { get; set; }     // 分期数
        public int BHavePay { get; set; }   // 已还期数
        public String BRemark { get; set; }  // 备注
        public Boolean BFinish { get; set; }    // 是否还清
    }
}