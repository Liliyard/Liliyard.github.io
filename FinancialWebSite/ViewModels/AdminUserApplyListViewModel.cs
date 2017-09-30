using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class AdminUserApplyListViewModel : BaseViewModel
    {
        public List<AdminApplyViewModel> Applys { get; set; }
    }
}