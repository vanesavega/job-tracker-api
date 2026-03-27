using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobTrackerApi.Data;
using JobTrackerApi.Models;

namespace JobTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplication>>> GetApplications([FromQuery] ApplicationStatus? status)
        {
            var query = _context.JobApplications.Include(j => j.Company).AsQueryable();

            if (status.HasValue)
                query = query.Where(j => j.Status == status.Value);

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplication>> GetApplication(int id)
        {
            var application = await _context.JobApplications
                .Include(j => j.Company)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (application == null) return NotFound();
            return application;
        }

        [HttpPost]
        public async Task<ActionResult<JobApplication>> CreateApplication(JobApplication application)
        {
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, application);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, JobApplication application)
        {
            if (id != application.Id) return BadRequest();

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.JobApplications.AnyAsync(j => j.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);
            if (application == null) return NotFound();

            _context.JobApplications.Remove(application);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}