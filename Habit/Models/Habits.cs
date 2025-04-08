namespace Habit.Models
{
    public class Habits
    {
        public int Id { get; set; }
        public string HabitName { get; set; }
        public string Frequency { get; set; }
        public DateOnly StartDate { get; set; }
        public int CompletedDays { get; set; }
        public int TargetDays { get; set; }
        public bool IsCompleted { get; set; }
    }
}
