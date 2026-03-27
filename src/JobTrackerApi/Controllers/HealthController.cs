using Microsoft.AspNetCore.Mvc;
using JobTrackerApi.Data;

namespace JobTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HealthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> CheckDatabase()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                if (canConnect)
                    return Ok(new { status = "Healthy", database = "Connected" });

                return StatusCode(503, new { status = "Unhealthy", database = "Cannot connect" });
            }
            catch (Exception ex)
            {
                return StatusCode(503, new { status = "Unhealthy", error = ex.Message });
            }
        }
    }
}