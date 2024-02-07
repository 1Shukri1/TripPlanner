namespace ExcursionOrganizer.Data.Models
{
    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString(); // Генериране на уникално id за всяка снимка. Представлява името на снимката
        }
        public string Id { get; set; } //име на снимката. Уникално id
        public string Extention { get; set; } // Разширение на снимката (например, jpg, png и др.)
        public int ExcursionId { get; set; } // Външен ключ към таблицата с екскурзии
        public virtual Excursion Excursion { get; set; } // Връзка M:1 (много към едно) с Excursion. Всяка снимка е свързана с конкретна екскурзия.
    }
}
