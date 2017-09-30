using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class OperateListViewModel : BaseViewModel
    {
        public List<OperateViewModel> Operates { get; set; }
    }
}