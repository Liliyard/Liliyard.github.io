using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class ContentionListViewModel : BaseViewModel
    {
        public List<ContentionViewModel> Contents { get; set; }
        public String Title { get; set; }
    }
}