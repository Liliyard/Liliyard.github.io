using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class BorrowListViewModel : BaseViewModel
    {
        public List<BorrowViewModel> borrows { get; set; }
    }
}