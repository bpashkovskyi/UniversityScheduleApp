using Microsoft.AspNetCore.Mvc;
using UniversityScheduleApp.Data.Models;

namespace UniversityScheduleApp.Controllersers
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