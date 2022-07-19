using Postermania.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Postermania.DAL
{
    public class PosterManiaContext : DbContext
    {
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
    }
}