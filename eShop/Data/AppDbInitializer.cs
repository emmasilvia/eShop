using System.Collections.Generic;
using System.Linq;
using eShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //Restaurante
                if (!context.Restaurante.Any())
                {
                    context.Restaurante.AddRange(new List<Restaurant>()
                    {
                        new Restaurant()
                        {
                            Nume = "Restaurante 1",
                            Poza = "https://img.freepik.com/premium-vector/restaurant-logo-design-template_79169-56.jpg?w=2000",
                            Descriere = "Descriere",
                        },
                        new Restaurant()
                        {
                            Nume = "Restaurante 2",
                            Poza = "https://marketplace.canva.com/EAESMsqG9rI/3/0/1600w/canva-grey-%26-green-elegant-minimal-good-taste-food-restaurant-logo-jeSR74GRMC8.jpg",
                            Descriere = "Descriere"
                        },
                        new Restaurant()
                        {
                            Nume = "Restaurante 3",
                            Poza = "https://media.istockphoto.com/id/981368726/vector/restaurant-food-drinks-logo-fork-knife-background-vector-image.jpg?s=612x612&w=0&k=20&c=9M26CBkCyEBqUPs3Ls5QCjYLZrB9sxwrSFmnAmNCopI=",
                            Descriere = "Descriere"
                        },
                    });
                    context.SaveChanges();
                }
                //Adrese
                if (!context.Adrese.Any())
                {
                    context.Adrese.AddRange(new List<Adresa>()
                    {
                        new Adresa
                        {
                            Strada = "Bl.Mamaia 256",
                            Oras = "Constanta",
                            RestaurantId = 1
                        },
                        new Adresa
                        {
                            Strada = "Strada Macului 23",
                            Oras = "Constanta",
                            RestaurantId = 2
                        },
                          new Adresa
                        {
                            Strada = "Strada Olteniei 4",
                            Oras = "Constanta",
                            RestaurantId = 3
                        },
                    });
                    context.SaveChanges();
                }
                //Ingrediente
                if (!context.Ingrediente.Any())
                {
                    context.Ingrediente.AddRange(new List<Ingredient>()
                    {
                        new Ingredient()
                        {
                            Denumire = "Ingrediente 1",

                        },
                          new Ingredient()
                        {
                            Denumire = "Ingrediente 2",

                        },
                    });
                    context.SaveChanges();
                }
                //Produse
                if (!context.Produse.Any())
                {
                    context.Produse.AddRange(new List<Produs>()
                    {
                        new Produs()
                        {
                            Denumire = "Produs1",
                            Descriere = "Descriere",
                            Pret = 39.50,
                            Imagine = "https://media.kaufland.com/images/PPIM/AP_Content_1010/std.lang.all/66/67/Asset_3306667.jpg",
                            RestaurantId = 3,
                        },
                           new Produs()
                        {
                            Denumire = "Produs2",
                            Descriere = "Descriere",
                            Pret = 39.50,
                            Imagine = "https://natashaskitchen.com/wp-content/uploads/2020/05/Vanilla-Cupcakes-3.jpg",
                            RestaurantId = 3,
                        },
                    });
                    context.SaveChanges();
                }
                //Produse_Ingrediente
                if (!context.Produse_Ingrediente.Any())
                {
                    context.Produse_Ingrediente.AddRange(new List<Produs_Ingredient>()
                    {
                        new Produs_Ingredient()
                        {
                            IngredientId = 1,
                            ProdusId = 1
                        },
                           new Produs_Ingredient()
                        {
                            IngredientId = 1,
                            ProdusId = 2
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
