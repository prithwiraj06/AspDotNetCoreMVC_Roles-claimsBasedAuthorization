using MvcPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPractice.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public string PageTitle { get; set; }
    }
}
