using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPractice.CustomValidator
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        public readonly string _allowedDomain;
        public ValidEmailDomainAttribute(string allowedDomain)
        {
            _allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
            string[] emailDomain = value.ToString().Split('@');
            return emailDomain[1].ToUpper() == _allowedDomain.ToUpper();         
        }
    }
}
