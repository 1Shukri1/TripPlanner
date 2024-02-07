namespace ExcursionOrganizer.Data.Models
{
    //междинна таблица - Екскурзия и потребител. Съхранява записите за екскурзиите
    public class ExcursionUser
    {
        public int Id { get; set; }

        public string? Name { get; set; } // Име на потребителя, записан за екскурзията

        public string? Phone { get; set; }// Телефон на потребителя, записан за екскурзията

        public int ExcursionId { get; set; }
        public virtual Excursion Excursion { get; set; }// Връзка М:1 (много към едно) с Excursion. Една екскурзия може да има много записи за потребители.

        public string UserId { get; set; }
        public virtual User User { get; set; } // Връзка М:1 (много към едно) с User. Един потребител може да е записан за много екскурзии.


    }
}
