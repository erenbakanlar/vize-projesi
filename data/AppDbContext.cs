using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using odev.dagitim.portali.models;

namespace odev.dagitim.portali.data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Odev> Odevler { get; set; }
        public DbSet<DagitilanOdev> DagitilanOdevler { get; set; }
    }
}
