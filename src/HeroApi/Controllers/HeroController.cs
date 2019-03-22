using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroApi.Models;

namespace HeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly HeroContext _context;

        public HeroController(HeroContext context)
        {
            _context = context;

            if (_context.Heroes.Count() == 0)
            {
                // Create a new Hero if collection is empty,
                // which means you can't delete all Heroes.
                _context.Heroes.Add(new Hero { Name = "Iron Man", Identity = "Tony Stark", Hometown = "L.A.", Age = 40 });
                _context.SaveChanges();
            }
        }
    }
}