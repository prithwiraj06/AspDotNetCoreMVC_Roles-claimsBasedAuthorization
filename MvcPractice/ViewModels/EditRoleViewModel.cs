using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPractice.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            UserNames = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<string> UserNames { get; set; }
    }
}
