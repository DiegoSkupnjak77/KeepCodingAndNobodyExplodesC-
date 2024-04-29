using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Model
{
    public class PersistanceContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=VDUS4DEVWIN7173\\SQLEXPRESS;Database=BombingGame;Trusted_Connection=True;TrustServerCertificate=True;");

        public DbSet<Color> Color { get; set; }
        public DbSet<Diameter> Diameter { get; set; }
        public DbSet<Length> Length { get; set; }
        public DbSet<Wire> Wire { get; set; }
        public DbSet<Material> Material { get; set; }
    }
}
