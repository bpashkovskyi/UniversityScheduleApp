using Microsoft.AspNetCore.Mvc;
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
    }
}