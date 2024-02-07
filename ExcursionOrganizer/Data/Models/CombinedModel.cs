using ExcursionOrganizer.Models;

//комбиниран модел за използване на 2 модела в един изглед
namespace ExcursionOrganizer.Data.Models
{
    public class CombinedModel
    {
        public InputExcursionModel ExcursionModel { get; set; }
        public InputExcursionUserModel ExcursionUserModel { get; set; }
    }
}
