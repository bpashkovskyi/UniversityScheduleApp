using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UniversityScheduleApp.Models;

namespace UniversityScheduleApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly UniversityContext _context;

        public ScheduleController(UniversityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Rooms()
        {
            var rooms = _context.Rooms.ToList();
            return View(rooms);
        }

        public IActionResult Teachers()
        {
            var teachers = _context.Teachers.ToList();
            return View(teachers);
        }

        public IActionResult Groups()
        {
            var groups = _context.Groups.ToList();
            return View(groups);
        }

        public IActionResult RoomSchedule(int id)
        {
            var schedule = _context.Schedules.Where(s => s.Object == id.ToString()).ToList();
            return View(schedule);
        }

        public IActionResult TeacherSchedule(int id)
        {
            var schedule = _context.Schedules.Where(s => s.Object == id.ToString()).ToList();
            return View(schedule);
        }

        public IActionResult GroupSchedule(int id)
        {
            var schedule = _context.Schedules.Where(s => s.Object == id.ToString()).ToList();
            return View(schedule);
        }
    }
}