namespace ExcursionOrganizer.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }// Потребителско име
        public string Email { get; set; }// Електронна поща
        public string Role { get; set; }// Роля на потребителя

        public bool IsDeleted { get; set; } //дали акаунтът на потребителя е изтрит
    }
}
