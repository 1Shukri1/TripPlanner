using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace ExcursionOrganizer.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Excursions = new HashSet<ExcursionUser>();
            Roles = new HashSet<IdentityUserRole<string>>();
            Claims = new HashSet<IdentityUserClaim<string>>();
            Logins = new HashSet<IdentityUserLogin<string>>();
            this.Id = Guid.NewGuid().ToString(); // Генериране на уникално id за всеки потребител
        }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public string Name { get; set; } // Име на потребителя
        public bool IsDeleted { get; set; } // За проверка за изтрит потребител
         
        public virtual ICollection<ExcursionUser> Excursions { get; set; } // Връзка 1:М (едно към много) с ExcursionUser. Един потребител може да бъде свързан с много екскурзии.


    }
}
