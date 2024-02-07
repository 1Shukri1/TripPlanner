using ExcursionOrganizer.Data;
using ExcursionOrganizer.Data.Models;
using ExcursionOrganizer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ExcursionOrganizer.Controllers
{
    public class AdminController : Controller
    {
        public readonly ApplicationDbContext db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext db, UserManager<User> um, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //public IActionResult LoginError()//ако акаунтът на потребителя е изтрит от администратора, ще се препрати към този екшън
        //{
        //    return View();
        //}
        [Authorize(Roles = "Admin")]
        public IActionResult UsersList(string id, UserViewModel model)
        {
            var userFd = db.Users.FirstOrDefault(r => r.Id == id);
            var usersWithRoles = (from user in db.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      IsDeleted = model.IsDeleted
                                  }).ToList().Select(p => new UserViewModel()
                                  {
                                      Id = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      IsDeleted = true,
                                  });
            return View(usersWithRoles);
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public IActionResult DeleteUserAccount(string id)
        //{

        //    var userFd = db.Users.FirstOrDefault(r => r.Id == id); //Търсене на потребител по айди
        //    if (userFd.IsDeleted == false)
        //    {
        //        userFd.IsDeleted = true;
        //    }
        //    else
        //    {
        //        userFd.IsDeleted = false;
        //    }

        //    db.Update(userFd);
        //    db.SaveChanges();
        //    return RedirectToAction("UsersList");
        //}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId);

            if (userToDelete == null)
            {
                // Потребителят не е намерен
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(userToDelete);

            if (result.Succeeded)
            {
                // Успешно изтриване на потребителя
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Възникна грешка при изтриване на потребителя
                return BadRequest("Възникна грешка при изтриване на потребителя");
            }
        }

        public IActionResult ExpiredExcursions()
        {
            var model = db.Excursions.Where(x => x.StartDate < DateTime.Today || x.Status == "Неактивна").Select(x => new InputExcursionModel
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Destination = x.Destination,
                Description = x.Description,
                Price = x.Price,
                Transport = x.Transport,
                MaxParticipants = x.MaxParticipants,
                Status = x.Status,
                Id = x.Id,
                ImgURL = x.Images.FirstOrDefault() != null ? $"/img/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extention}" : null, //прочитене на снимката от базата данни
            }).ToList();

            return View(model);
        }
    }
}

