using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Postermania.Models
{
    public class Dimension
    {
        public int ID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Poster> Posters { get; set; }

        public string Name
        {
            get { return $"{Width}x{Height}"; }
        }
    }
}