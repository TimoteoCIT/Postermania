using System;
using System.Collections.Generic;
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
        public decimal BasePrice { get; set; }
        public decimal PricePerCm { get; set; }
        public ItemType type { get; set; }
        public byte[] Image { get; set; }
        public List<Dimension> Dimensions { get; set; }
    }
}