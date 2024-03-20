using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Homes.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly HomesContext _context;

        public HomesController(HomesContext context)
        {
            _context = context;
        }

        // GET: api/Homes
        [Route("GetApplicants")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
        {
            return await _context.Applicants.ToListAsync();
        }

        //GET: users
        [Route("GetUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(){
            return await _context.Users.ToListAsync();
        }

        //GET Provinces
        [Route("GetProvinces")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvinces()
        {
            return await _context.Provinces.ToListAsync();
        }

        // GET: api/Homes/5

        [HttpGet("GetApplicant/{id}")]
        
        public async Task<ActionResult<Applicant>> GetApplicant(int id)
        {
            var applicant = await _context.Applicants.FindAsync(id);

            if (applicant == null)
            {
                return NotFound();
            }

            return applicant;
        }

        // PUT: api/Homes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPut("EditApplicant/{id}")]
        public async Task<IActionResult> PutApplicant(int id, Applicant applicant)
        {
            if (id != applicant.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Homes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("CreateApplicant")]
        [HttpPost]
        public async Task<ActionResult<Applicant>> PostApplicant(Applicant applicant)
        {
            _context.Applicants.Add(applicant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicant", new { id = applicant.Id }, applicant);
        }
        // POST: Api/Homes
        [Route("SignUp")]
        [HttpPost]
        public async Task<ActionResult<User>> SignUp(User user)
        {   
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.Email }, user);
        }


        // DELETE: api/Homes/5
        [HttpDelete("DeleteApplicant/{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }

            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicants.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("Checkuser/ {email}")]
        public async Task<IActionResult> signin([FromRoute] string email, string password)
        {
            var useremail = await _context.Users.FindAsync(email);
            var userpassword = await _context.Users.FindAsync(password);
            if (useremail == null)
            {
                return Ok("email not found");
            }

            return Ok(useremail);
        }


    }
}
