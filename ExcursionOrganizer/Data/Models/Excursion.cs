//суздаване на таблица за екскурзиите
namespace ExcursionOrganizer.Data.Models
{
    public class Excursion
    {
        public Excursion()
        {
            // Инициализация на колекциите за изображения и потребители
            this.Images = new HashSet<Image>();
            this.Users = new HashSet<ExcursionUser>();
        }
        //колони в таблица екскурзии (в базата данни)
        public int Id { get; set; }
        public string Destination { get; set; } // дестинация
        public double Price { get; set; } // цена
        public DateTime StartDate { get; set; } // начална дата
        public DateTime EndDate { get; set; } // крайна дата
        public string Description { get; set; } //описание
        public string Transport { get; set; } // транспорт
        public int MaxParticipants { get; set; } // Максимален брой участници
        public string? Status { get; set; } //Състояние

        // Една екскурзия може да има много потребители, записани за нея.
        public virtual ICollection<ExcursionUser> Users { get; set; } // Връзка 1:М (едно към много) между Excursion и ExcursionUser.
        
        // Една екскурзия може да има много изображения, свързани с нея.
        public virtual ICollection<Image> Images { get; set; } // Връзка 1:М (едно към много) между Excursion и Image.
    }
}
