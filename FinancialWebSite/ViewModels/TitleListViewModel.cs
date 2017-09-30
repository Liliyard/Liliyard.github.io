using FinancialWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class TitleListViewModel : BaseViewModel
    {
        public List<TitleViewModel> Titles { get; set; }
        public List<User> Users { get; set; }
    }
}