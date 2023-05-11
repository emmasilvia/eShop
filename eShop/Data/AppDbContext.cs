using eShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Restaurant>()
          .HasOne(r => r.Adresa)
          .WithOne(a => a.Restaurant)
          .HasForeignKey<Adresa>(a => a.RestaurantId)
          .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Produs>()
                .HasOne(p => p.Restaurant)
                .WithMany(r => r.listaProduse)
                .HasForeignKey(p => p.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Produs_Ingredient>().HasKey(pi => new
            {
                pi.ProdusId,
                pi.IngredientId
            });

            modelBuilder.Entity<Produs_Ingredient>().HasOne(p => p.Produs).WithMany(pi => pi.listaProduse_Ingrediente).HasForeignKey(p => p.ProdusId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Produs_Ingredient>().HasOne(i => i.Ingredient).WithMany(pi => pi.listaProduse_Ingrediente).HasForeignKey(i => i.IngredientId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Ingredient> Ingrediente { get; set; }
        public DbSet<Produs> Produse { get; set; }
        public DbSet<Produs_Ingredient> Produse_Ingrediente { get; set; }
        public DbSet<Restaurant> Restaurante { get; set; }

        public DbSet<Adresa> Adrese { get; set; }

    }
}
