using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Postermania.Models
{
    public class User
    {
        public int ID { get; set; }
        [Display(Name="Username")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}