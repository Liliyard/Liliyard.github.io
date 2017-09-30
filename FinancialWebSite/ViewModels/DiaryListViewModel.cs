using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class DiaryListViewModel : BaseViewModel
    {
        public List<DiaryViewModel> Diaries { get; set; }
    }
}