using System.Linq;
using System.Threading.Tasks;
using EskobInnovation.IdeaManagement.API.Attributes;
using EskobInnovation.IdeaManagement.API.Data;
using EskobInnovation.IdeaManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EskobInnovation.IdeaManagement.API.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class SiteController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public SiteController(ApplicationDbContext context)
    {
      _context = context;
    }
    // GET: api/Site/5
    [ApiKey]
    [HttpGet("{id}")]
    public async Task<ActionResult<Site>> GetSite(int id)
    {
        var site = await _context.Sites.FindAsync(id);

        if (site == null)
        {
            return NotFound();
        }
        return site;
    }
        // PUT: api/Site/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [ApiKey]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSite(int id, Site site)
    {
      if (id != site.SiteId)
      {
        return BadRequest();
      }
        var entity = await _context.Sites
            .FindAsync(id);

        if (id != entity.SiteId)
        {
            return NotFound();
        }

        _context.Entry(entity).CurrentValues.SetValues(site);
        _context.Entry(entity).State = EntityState.Modified;
        try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SiteExists(id))
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

    // POST: api/Site
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [ApiKey]
    [HttpPost]
    public async Task<ActionResult<Site>> PostSite(Site site)
    {
      _context.Sites.Add(site);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetSite", new { id = site.SiteId }, site);
    }

    [ApiKey]
    // DELETE: api/Site/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Site>> DeleteSite(int id)
    {
      var site = await _context.Sites.FindAsync(id);
      if (site == null)
      {
        return NotFound();
      }

      _context.Sites.Remove(site);
      await _context.SaveChangesAsync();

      return site;
    }

    [HttpGet("getbylink/{link}")]
    public async Task<ActionResult<Site>> GetSiteByLink(string link)
    {
      Site site = await _context.Sites
        .Where(s => s.Link == link)
        .FirstOrDefaultAsync();

      return site;
    }

    private bool SiteExists(int id)
    {
      return _context.Sites.Any(e => e.SiteId == id);
    }
  }
}
