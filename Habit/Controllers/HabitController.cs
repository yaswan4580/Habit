using Microsoft.EntityFrameworkCore;  

using System.Linq;
using Habit.Data;
using Microsoft.AspNetCore.Mvc;
using Habit.Models;

namespace Habit.Controllers
{
    public class HabitController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HabitController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var habit= await _context.Habit
                .Where(h=>h.CompletedDays < h.TargetDays)
                .ToListAsync();
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");
            return View(habit);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Habits habit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(habit);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var habit = await _context.Habit.FindAsync(id);
            if (habit == null) return NotFound();

            return View(habit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Habits habit)
        {
            if (id != habit.Id || !ModelState.IsValid) return View(habit);

            var existingHabit = await _context.Habit.FindAsync(id);
            if (existingHabit == null) return NotFound();

            _context.Entry(existingHabit).CurrentValues.SetValues(habit);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var habit = await _context.Habit.FindAsync(id);
            if (habit == null) return NotFound();

            return View(habit);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habit = await _context.Habit.FindAsync(id);
            if (habit != null)
            {
                _context.Habit.Remove(habit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("Data")]
        public async Task<IActionResult> Index(string filter)
        {
            var habits = _context.Habit
                .Where(h => h.CompletedDays < h.TargetDays); 

            if (!string.IsNullOrEmpty(filter) && filter != "All")
            {
                habits = habits.Where(h => h.Frequency == filter);
            }

            var filteredList = await habits.ToListAsync();
            return View(filteredList);
        }

        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var habit =await _context.Habit.FindAsync(id);
            if (habit==null) return NotFound();
            habit.CompletedDays += 1;
            _context.Habit.Update(habit);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
