using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Postermania.Models
{
    public enum ItemType
    {
        Poster,
        Scroll,
        Framed
    }

    public class Poster
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Display(Name = "Base Price")]
        public decimal BasePrice { get; set; }
        [Display(Name = "Price per CM")]
        public decimal PricePerCm { get; set; }
        public ItemType Type { get; set; }
        public byte[] Image { get; set; }
        public List<Dimension> Dimensions { get; set; }
    }
}