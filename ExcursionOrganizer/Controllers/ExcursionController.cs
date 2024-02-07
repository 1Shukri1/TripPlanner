using ExcursionOrganizer.Data;
using ExcursionOrganizer.Data.Models;
using ExcursionOrganizer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExcursionOrganizer.Controllers
{
    public class ExcursionController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<User> _userManager;
        private string[] allowedExtention = new[] { "png", "jpg", "jpeg" };
        public ExcursionController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager) /*: base(userManager)*/
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            // Извличане на екскурзии от базата данни, където началната дата е след днешната дата или крайната дата е преди началната дата
            var model = db.Excursions.Where(x => x.StartDate > DateTime.Today || x.EndDate <= x.StartDate).Select(x => new InputExcursionModel
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
                // Формиране на URL за изображението, ако такова съществува в базата данни
                ImgURL = x.Images.FirstOrDefault() != null ? $"/img/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extention}" : null, //прочитене на снимката от базата данни
            }).ToList();

            // Връщане на модела към изгледа
            return View(model);
        }

        [HttpGet]
        public IActionResult Add() //при стартирането на съответната страница
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Администраторско разрешение
        public async Task<IActionResult> Add(InputExcursionModel model)//при кликването на бутона "Добави екскурзии" във формата за изпращене към сървъра
        {
            var excursion = new Excursion
            {
                Destination = model.Destination,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Price = model.Price,
                Description = model.Description,
                Transport = model.Transport,
                MaxParticipants = model.MaxParticipants,
                Status = model.Status
            };
            // // Определяне на разширението на качената снимка, което ще се използва за името на файла 
            var extention = Path.GetExtension(model.Image.FileName).TrimStart('.');
            //създаване на обект, който ще се запише в БД
            var image = new Image
            {
                Extention = extention
            };

            //съхранява информация за снимката
            string path = $"{webHostEnvironment.WebRootPath}/img/{image.Id}.{extention}";

            //Задава се пълният път към съхранението на снимката във файловата система.
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                model.Image.CopyTo(fs);
            }

            //Добавяне на снимката към екскурзията и запис в базата данни
            excursion.Images.Add(image);
            await db.Excursions.AddAsync(excursion);
            await db.SaveChangesAsync();

            //Пренасочване към началната страница
            return this.RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            // Извличане на данни за екскурзията от базата данни
            var excursionModel = db.Excursions
                .Where(x => id == x.Id)
                .Select(x => new InputExcursionModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    Transport = x.Transport,
                    Description = x.Description,
                    Destination = x.Destination,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = x.Status,
                    MaxParticipants = x.MaxParticipants,
                    ImgURL = $"/img/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extention}",
                })
                .FirstOrDefault();

            // Вземане на id-то номер на текущия потребител
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Извличане на данни за потребителя от междинната таблица, която е за съхранение на записите за екскурзиите
            var excursionUserModel = db.ExcursionUsers
                .Where(eu => eu.ExcursionId == id && eu.UserId == userId)
                .Select(eu => new InputExcursionUserModel
                {
                    Id = eu.Id,
                    Name = eu.Name,
                    Phone = eu.Phone
                })
                .FirstOrDefault();

            // Създаване на комбиниран модел, съдържащ информацията за екскурзията и потребителя
            var combinedModel = new CombinedModel
            {
                ExcursionModel = excursionModel,
                ExcursionUserModel = excursionUserModel
            };

            // Връщане на изгледа с комбинирания модел
            return View(combinedModel);
        }


        // HTTP Get метод за редакция на екскурзия
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Извличане на екскурзията от базата данни по зададен идентификационен номер
            var excursion = db.Excursions.Where(s => s.Id == id).FirstOrDefault();

            // Създаване на модел от данните на екскурзията
            var model = new InputExcursionModel
            {
                Id = excursion.Id,
                Description = excursion.Description,
                Price = excursion.Price,
                Transport = excursion.Transport,
                Destination = excursion.Destination,
                EndDate = excursion.EndDate,
                Status = excursion.Status,
                StartDate = excursion.StartDate,
                MaxParticipants = excursion.MaxParticipants,
                //ImgURL = $"/img/{excursion.Images.FirstOrDefault().Id}.{excursion.Images.FirstOrDefault().Extention}"
            };

            return this.View(model); // Връщане на изгледа за редакция със създадения модел
        }

        // HTTP Post метод за редакция на екскурзия
        [HttpPost]
        public IActionResult Edit(int id, InputExcursionModel model)
        {
            var excursion = db.Excursions.Where(s => s.Id == model.Id).FirstOrDefault(); // Извличане на екскурзията от базата данни за редакция

            // Актуализация на данните на екскурзията от модела
            excursion.Id = model.Id;
            excursion.Description = model.Description;
            excursion.Price = model.Price;
            excursion.Destination = model.Destination;
            excursion.EndDate = model.EndDate;
            excursion.Transport = model.Transport;
            excursion.StartDate = model.StartDate;
            excursion.Status = model.Status;
            excursion.MaxParticipants = model.MaxParticipants;
            //excursion.Images = model.ImgURL;
            //model.ImgURL = $"/img/{excursion.Images.FirstOrDefault().Id}.{excursion.Images.FirstOrDefault().Extention}";

            db.SaveChanges(); // Запазване на промените в базата данни

            // препращане към списъка с екскурзии след успешно редактиране
            return this.RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var excursion = db.Excursions.Where(s => s.Id == id).FirstOrDefault(); //извличане на екскурзията от базата данни чрез id
            db.Excursions.Remove(excursion);//изтриване на екскурзията от базата данни
            db.SaveChanges(); // Запазване на промените в базата данни
            return RedirectToAction("Index"); //Препращане към началната страница
        }


        [HttpPost]
        public async Task<IActionResult> RegisterForExcursion(int excursionId, InputExcursionUserModel model)
        {
            // Вземане на текущия потребител
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Проверка дали потребителят вече е записан за тази екскурзия и не е достигнал лимита
            var userExcursionsCount = await db.ExcursionUsers.CountAsync(eu => eu.UserId == userId);
            if (userExcursionsCount >= 2)
            {
                return BadRequest("Не можете да се запишете за повече от 2 екскурзии!");
            }

            // Проверка дали потребителят вече е записан за тази екскурзия
            if (await db.ExcursionUsers.AnyAsync(eu => eu.ExcursionId == excursionId && eu.UserId == userId))
            {
                return BadRequest("Потребителят вече е записан за тази екскурзия.");
            }

            // Създаване на нов запис за междинната таблица ExcursionUser
            var excursionUser = new ExcursionUser
            {
                ExcursionId = excursionId,
                UserId = userId,
                Name = model.Name,
                Phone = model.Phone
            };

            // Извличане на екскурзията от базата данни по id
            var excursionFd = await db.Excursions.FirstOrDefaultAsync(r => r.Id == excursionId);

            // Проверка дали е намерена екскурзията и дали има свободни места за участие
            if (excursionFd != null && excursionFd.MaxParticipants >= 1)
            {
                excursionFd.MaxParticipants--; // Намаляване на броя на свободните места за участници
                await db.SaveChangesAsync(); // Запазване на промените в базата данни
            }

            // Добавяне и запазване на записа в базата данни
            db.ExcursionUsers.Add(excursionUser);
            await db.SaveChangesAsync();

            return RedirectToAction("Index"); // Редирект към началната страница
        }

        [HttpPost]
        //[Authorize] // изисква логнат потребител
        public async Task<IActionResult> UnsubscribeFromExcursion(int excursionId)
        {
            // Взимане на текущия потребител
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Проверка дали потребителят е записан за тази екскурзия
            var excursionUser = await db.ExcursionUsers
                .FirstOrDefaultAsync(eu => eu.ExcursionId == excursionId && eu.UserId == userId);

            if (excursionUser == null)
            {
                return BadRequest("Потребителят не е записан за тази екскурзия.");
            }

            // Изтриване на записа от базата данни
            db.ExcursionUsers.Remove(excursionUser);
            await db.SaveChangesAsync();

            // Извличане на екскурзията от базата данни по id
            var excursionFd = await db.Excursions.FirstOrDefaultAsync(r => r.Id == excursionId);

            if (excursionFd != null)
            {
                excursionFd.MaxParticipants++; // Увеличаване на броя на свободните места за екскурзията
                await db.SaveChangesAsync(); // Запазване на промените в базата данни
            }

            return RedirectToAction("Index"); // Редирект към началната страница
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Администраторско разрешение
        public async Task<IActionResult> UnsubscribeUserFromExcursionByAdmin(int excursionId)
        {
            //Взимане на id - то на текущия потребител
            var userId = User.FindAll(ClaimTypes.NameIdentifier);

            var excursionUserToRemove = db.ExcursionUsers.FirstOrDefault(eu => eu.ExcursionId == excursionId);
            if (excursionUserToRemove != null)
            {
                db.ExcursionUsers.Remove(excursionUserToRemove);
                db.SaveChanges();

                // Увеличаване на броя на свободните места за екскурзията
                var excursionFd = db.Excursions.FirstOrDefault(r => r.Id == excursionId);
                if (excursionFd != null)
                {
                    excursionFd.MaxParticipants++;
                    db.SaveChanges(); // Запазване на промените в базата данни
                }
            }

            // Назад към началната страница за екскурзии
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        public IActionResult RegisteredUsers(int excursionId) // визуализиране на записаните потребители на дадена екскурзия
        {
            var registeredUsers = db.ExcursionUsers
            .Where(eu => eu.ExcursionId == excursionId) // Филтриране по идентификатор на екскурзия
            .Select(eu => eu.User) // Извличане на потребителите, свързани с регистрациите за тази екскурзия
            .ToList();

            ViewBag.ExcursionId = excursionId; // Предаване на excursionId в ViewBag
            //ViewBag е динамичен обект в ASP.NET MVC, който позволява пренос на данни от контролера към изгледа. 

            return View(registeredUsers);
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyExcursions() //екшън за визуализиране на екскурзиите в които са записани текущите потребители
        {
            // Вземане на текущия потребител
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await db.Excursions.Where(e => e.Users.Any(u => u.UserId == userId) && e.StartDate > DateTime.Today && e.Status != "Неактивна")
                .Select(x => new InputExcursionModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    Transport = x.Transport,
                    Description = x.Description,
                    Destination = x.Destination,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = x.Status,
                    MaxParticipants = x.MaxParticipants,
                    ImgURL = $"/img/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extention}", //прочитене на снимката от базата данни
                }
             ).ToListAsync();
            return View(model);
        }
    }
}
