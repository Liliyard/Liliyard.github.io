using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class AccountListViewModel : BaseViewModel
    {
        public List<AccountViewModel> Accounts { get; set; }
        public String Today { get; set; }
    }
}