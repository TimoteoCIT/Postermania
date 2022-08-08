using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Postermania.Models
{
    public enum CardBrand
    {
        MasterCard,
        Visa,
        AmericanExpress
    }

    public class CreditCard
    {
        public int ID { get; set; }
        public CardBrand Brand { get; set; }
        public int Number { get; set; }
        public int Secret { get; set; }
    }
}