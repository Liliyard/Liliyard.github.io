using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class AdminUserManageListViewModel : BaseViewModel
    {
        public List<AdminManageViewModel> Admins { get; set; }
    }
}