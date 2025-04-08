using System.Data.Entity;
using Habit.Data;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Controllers
{
    public class CompletedController : Controller
    {
        private readonly ApplicationDbContext _appcontext;
        public CompletedController(ApplicationDbContext appcontext)
        {
            _appcontext = appcontext;
        }
        public IActionResult Completed()
        {
            var c = _appcontext.Habit
                .Where(h => h.CompletedDays == h.TargetDays)
                .ToList();
            return View(c);
        }
    }
}
