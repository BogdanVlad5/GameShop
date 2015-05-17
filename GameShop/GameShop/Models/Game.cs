using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameShop.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
    }
    public class GameDBContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}