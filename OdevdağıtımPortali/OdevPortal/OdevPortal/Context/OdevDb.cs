using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OdevPortal.Entity;

namespace OdevPortal.Context
{
    public class OdevDb:IdentityDbContext<Kullanicilar, AppRole , int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=OdevPortalDb1; TrustServerCertificate=True; Integrated Security=True;");
        }

        public DbSet<Ogrenci> ogrencis { get; set; }
        public DbSet<Ogretmen> ogretmens { get; set; }
        public DbSet<Ders> ders { get; set; }
        public DbSet<Odev> odevs { get; set; }
    }
}
