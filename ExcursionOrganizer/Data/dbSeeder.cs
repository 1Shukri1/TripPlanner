using ExcursionOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcursionOrganizer.Data
{
    public class dbSeeder
    {
        public static void SeedExcursions(ApplicationDbContext context)
        {
            if (!context.Excursions.Any())
            {
                var excursions = new List<Excursion>
            {
                new Excursion
                {
                    Destination = "Пловдив",
                    Price = 50.00,
                    StartDate = DateTime.Now.AddDays(7),
                    EndDate = DateTime.Now.AddDays(10),
                    Description = "Разглеждане на историческите забележителности",
                    Transport = "Автобус",
                    MaxParticipants = 20,
                    Status = "Активна",
                    Images = new List<Image>
                    {
                        new Image { Id = "bc85b2e7-ac03-4858-8dbc-311993057b1f" , Extention = ".jpg",  },
                    }
                },
            };

                context.Excursions.AddRange(excursions);
                context.SaveChanges();
            }
        }
    }
}
