using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteSettingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SiteSettingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SiteSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteSettings>>> GetSiteSettings()
        {
            return await _context.SiteSettings.ToListAsync();
        }

        // GET: api/SiteSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteSettings>> GetSiteSetting(int id)
        {
            var siteSetting = await _context.SiteSettings.FindAsync(id);

            if (siteSetting == null)
            {
                return NotFound();
            }

            return siteSetting;
        }

        // POST: api/SiteSettings
        [HttpPost]
        public async Task<ActionResult<SiteSettings>> PostSiteSetting(SiteSettings siteSetting)
        {
            _context.SiteSettings.Add(siteSetting);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSiteSetting), new { id = siteSetting.Id }, siteSetting);
        }

        // PUT: api/SiteSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiteSetting(int id, SiteSettings siteSetting)
        {
            if (id != siteSetting.Id)
            {
                return BadRequest();
            }

            _context.Entry(siteSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteSettingExists(id))
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

        // DELETE: api/SiteSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiteSetting(int id)
        {
            var siteSetting = await _context.SiteSettings.FindAsync(id);
            if (siteSetting == null)
            {
                return NotFound();
            }

            _context.SiteSettings.Remove(siteSetting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteSettingExists(int id)
        {
            return _context.SiteSettings.Any(e => e.Id == id);
        }
    }
}
