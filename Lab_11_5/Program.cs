using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lab_11_5
{
    class SakilaContext : DbContext
    {
        public DbSet<Film> Film { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=localhost\sqlexpress;Database=sakila;Trusted_Connection=True;");
        }
    }

    class Film
    {
        [Key]
        public int film_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string release_year { get; set; }
        public byte language_id { get; set; }
        public byte original_language_id { get; set; }
        public byte rental_duration { get; set; }
        public decimal rental_rate { get; set; }
        public Int16 length { get; set; }
        public decimal replacement_cost { get; set; }
        public string rating { get; set; }
        public string special_features { get; set; }
        public DateTime last_update { get; set; }


        public Film(string title, string description, string release_year, byte rental_duration,
            decimal rental_rate, Int16 length, decimal replacement_cost, string rating)
        {
            this.title = title;
            this.description = description;
            this.release_year = release_year;
            this.rental_duration = rental_duration;
            this.rental_rate = rental_rate;
            this.length = length;
            this.replacement_cost = replacement_cost;
            this.rating = rating;

            special_features = "Trailers";
            last_update = DateTime.Now;
            language_id = 1;
            original_language_id = 1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SakilaContext sakila = new SakilaContext();
            Film war1917 = new Film("1917", "2019 War Drama By Director Sam Mendes", "2019", 3, 5.99m, 179, 19.99m, "R");
            Film joker = new Film("Joker", "Oscar-Nominated SuperHero Drama", "2019", 3, 6.99m, 182, 23.99m, "R");
            Film starwars = new Film("Star Wars: The Rise of SkyWalker", "Trash Disney Fanfic", "2019", 3, 4.99m, 202, 21.99m, "PG-13");
            sakila.Film.Add(war1917);
            sakila.Film.Add(joker);
            sakila.Film.Add(starwars);
            sakila.SaveChanges();

            Film[] allfilms = sakila.Film.ToArray();

            var newfilms = allfilms.Where(x => x.release_year == "2019");

            StringBuilder Tohtml = new StringBuilder();
            Tohtml.Append("<html>\n");
            Tohtml.Append("  <head>");
            Tohtml.Append("    <title>Sakila New Films</title>\n");
            Tohtml.Append("  </head>\n");
            Tohtml.Append("  <body\n");
            Tohtml.Append("    <h1> New Films Coming Soon! </h1>\n");
            Tohtml.Append("      <ul>\n");

            foreach (var film in newfilms)
            {
                Tohtml.Append("<li>");
                Tohtml.Append(film.title + " " + film.description);
                Tohtml.Append("</li>");
            }

            Tohtml.Append("      </ul>\n");
            Tohtml.Append("  </body>\n");
            Tohtml.Append("</html>\n");

            string htmlFile = "C:\\Users\\jair_\\OneDrive\\Documents\\Lab_11_5.html";
            File.WriteAllText(htmlFile, Tohtml.ToString());
        }
    }
}



