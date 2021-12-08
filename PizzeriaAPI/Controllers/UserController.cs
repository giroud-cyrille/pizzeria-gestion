using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaAPI.Authorize;
using PizzeriaAPI.Models;
using ProjetcLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaAPI.Controllers
{
    [KeyRequirement]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context;

        public UserController(Context context)
        {
            _context = context;
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<User>> Get(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            if(id != user.Id)
                return BadRequest();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        bool UserExists(int id)
            => _context.Users.Any(x => x.Id == id);
    }
}
