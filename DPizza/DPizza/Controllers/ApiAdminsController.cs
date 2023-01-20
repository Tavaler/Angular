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
    public class ApiAdminsController : ControllerBase
    {
        private readonly DpizzaContext _context;

        public ApiAdminsController(DpizzaContext context)
        {
            _context = context;
        }

        // GET: api/ApiAdmins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
            return await _context.Admin.ToListAsync();
        }

        // GET: api/ApiAdmins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admin.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        [Route("AdminLogin")]
        [HttpPost]
        public async Task<ActionResult> AdminLogin([FromForm] Admin data)
        {
            var result = await _context.Admin.FirstOrDefaultAsync(p => p.AdminEmail.Equals(data.AdminEmail)
            && p.AdminPassword.Equals(data.AdminPassword));

            //if (result == null) return NotFound();
            if (result == null) return CreatedAtAction(nameof(AdminLogin), new { msg = "ไม่พบผู้ใช้" });

            return CreatedAtAction(nameof(AdminLogin), new { msg = "OK", result });
        }


    }
}

