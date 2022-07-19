using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Postermania.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public bool IsAdmin { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}