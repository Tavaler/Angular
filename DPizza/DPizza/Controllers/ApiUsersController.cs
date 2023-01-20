using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DPizza.Models.Data;

namespace DPizza.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        private readonly DpizzaContext _context;

        public ApiUsersController(DpizzaContext context)
        {
            _context = context;
        }

        // GET: api/ApiUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/ApiUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            var users = await _context.User.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }
        

        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromForm] User data)
        {
            var result = await _context.User.FirstOrDefaultAsync(p => p.UserEmail.Equals(data.UserEmail));

            if (result != null) return CreatedAtAction(nameof(Register), new { msg = "ชื่อผู้ใช้ซ้ำ" });

            await _context.User.AddAsync(data);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Register), data);
        }

        // ApiUsers/Login
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromForm] User data)
        {
            var result = await _context.User.FirstOrDefaultAsync(p => p.UserEmail.Equals(data.UserEmail) 
            && p.UserPassword.Equals(data.UserPassword));

            if (result == null) return CreatedAtAction(nameof(Login), new { msg = "ไม่พบผู้ใช้" });

            return CreatedAtAction(nameof(Login), new { msg = "OK", result });
        }

    }
}