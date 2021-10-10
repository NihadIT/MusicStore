using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public MusicStoreEntities(DbContextOptions<MusicStoreEntities> options): base(options)
        {
        }
        public MusicStoreEntities()
        {
        }
    }
}
