//// User API
///


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using DPizza.Data;

//namespace DPizza.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class ApiUsersController : ControllerBase
//    {
//        private readonly DpizzaContext _context;

//        public ApiUsersController(DpizzaContext context)
//        {
//            _context = context;
//        }

//        // GET: api/ApiUsers
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
//        {
//            return await _context.User.ToListAsync();
//        }

//        // GET: api/ApiUsers/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<User>> GetUsers(int id)
//        {
//            var users = await _context.User.FindAsync(id);

//            if (users == null)
//            {
//                return NotFound();
//            }

//            return users;
//        }
//        [Route("Login")]
//        [HttpPost]
//        public async Task<ActionResult> Login([FromForm] User data)
//        {
//            var result = await _context.User.FirstOrDefaultAsync(p => p.UserEmail.Equals(data.UserEmail) && p.UserPassword.Equals(data.UserPassword));

//            if (result == null) return CreatedAtAction(nameof(Login), new { msg = "ไม่พบผู้ใช้" });

//            return CreatedAtAction(nameof(Login), new { msg = "OK", beaver = result });
//        }

//        [Route("Register")]
//        [HttpPost]
//        public async Task<ActionResult> Register([FromForm] User data)
//        {
//            var result = await _context.User.FirstOrDefaultAsync(p => p.UserEmail.Equals(data.UserEmail));

//            if (result != null) return CreatedAtAction(nameof(Register), new { msg = "ชื่อผู้ใช้ซ้ำ" });

//            await _context.User.AddAsync(data);
//            await _context.SaveChangesAsync();
//            return CreatedAtAction(nameof(Register), data);
//        }



//    }
//}





////////////////////////////Admin  API

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using DPizza.Data;

//namespace DPizza.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class ApiAdminsController : ControllerBase
//    {
//        private readonly DpizzaContext _context;

//        public ApiAdminsController(DpizzaContext context)
//        {
//            _context = context;
//        }

//        // GET: api/ApiAdmins
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
//        {
//            return await _context.Admin.ToListAsync();
//        }

//        // GET: api/ApiAdmins/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Admin>> GetAdmin(int id)
//        {
//            var admin = await _context.Admin.FindAsync(id);

//            if (admin == null)
//            {
//                return NotFound();
//            }

//            return admin;
//        }

//        //[Route("AdminLogin")]
//        //[HttpPost]
//        //public async Task<ActionResult> AdminLogin([FromForm] Admin data)
//        //{
//        //    var result = await _context.Admin.FirstOrDefaultAsync(p => p.AdminEmail.Equals(data.AdminEmail)
//        //    && p.AdminPassword.Equals(data.AdminPassword));

//        //    //if (result == null) return NotFound();
//        //    if (result == null) return CreatedAtAction(nameof(AdminLogin), new { msg = "ไม่พบผู้ใช้" });

//        //    return CreatedAtAction(nameof(AdminLogin), new { msg = "OK", result });
//        //}


//    }
//}
