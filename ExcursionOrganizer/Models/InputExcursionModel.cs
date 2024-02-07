using ExcursionOrganizer.Data.Models;

namespace ExcursionOrganizer.Models
{
    public class InputExcursionModel
    {
        public int Id { get; set; } // id на екскурзията
        public string Destination { get; set; }// Дестинация
        public double Price { get; set; }// Цена
        public DateTime StartDate { get; set; }// Начална дата
        public DateTime EndDate { get; set; }// Крайна дата
        public string Description { get; set; }// Описание
        public IFormFile Image { get; set; }// Снимка
        public string ImgURL { get; set; }// URL адрес на снимката - id на снимката
        public string Transport { get; set; }// Вид транспорт
        public int MaxParticipants { get; set; } // Максимален брой участници
        public string? Status { get; set; } // Състояние (например: активна, неактивна)


        public string UserId { get; set; } // id на потребителя
        public User User { get; internal set; } // Навигационно свойство за свързване с потребителя

        public List<InputExcursionUserModel> excursionUser { get; set; }// Списък с участниците в екскурзията

    }
}
