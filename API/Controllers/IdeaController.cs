using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EskobInnovation.IdeaManagement.API.Data;
using EskobInnovation.IdeaManagement.API.Data.Migrations;
using EskobInnovation.IdeaManagement.API.Models;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EskobInnovation.IdeaManagement.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class IdeaController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public IdeaController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Idea
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Idea>>> GetIdeas()
    {
      return await _context.Ideas.ToListAsync();
    }

    // GET: api/Idea/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Idea>> GetIdea(int id)
    {
      var idea = await _context.Ideas.FindAsync(id);

      if (idea == null)
      {
        return NotFound();
      }

      return idea;
    }

    // GET: api/Idea
    [HttpGet("getsiteideas")]
    public async Task<ActionResult<IEnumerable<Idea>>> GetSiteIdeas(int siteId)
    {
      var ideas = await _context.Ideas
        .Where(i => i.SiteId == siteId)
        .ToListAsync();

      return ideas;
    }

    // PUT: api/Idea/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIdea(int id, Idea idea)
    {
      if (id != idea.IdeaId)
      {
        return BadRequest();
      }

      _context.Entry(idea).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!IdeaExists(id))
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

    // POST: api/Idea
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Idea>> PostIdea([FromForm] Idea idea, [FromForm] List<IFormFile> files, [FromForm] List<String> hashtags)
    {
      idea.Files = new List<Models.File>();
      foreach (var element in files)
      {
        if (element != null)
        {
          try
          {
            Models.File file = new Models.File();
            file.IdeaId = idea.IdeaId;
            file.Name = element.FileName;
            using (var ms = new MemoryStream())
            {
              element.CopyTo(ms);
              file.Data = ms.ToArray();
            }
            idea.Files.Add(file);
          }
          catch (System.Exception error)
          {
            System.Console.WriteLine(error);
          }
        }
      }

      idea.Hashtags = new List<Hashtag>();
      foreach (var element in hashtags)
      {
        if (!element.IsNullOrEmpty())
        {
          var hashtag = await _context.Hashtags
          .Where(h => h.Name == element)
          .FirstOrDefaultAsync();

          if (hashtag != null)
          {
            idea.Hashtags.Add(hashtag);
          }
          else
          {
            Hashtag h = new Hashtag();
            h.Name = element;
            idea.Hashtags.Add(h);
          }
        }
      }

      _context.Ideas.Add(idea);

      await _context.SaveChangesAsync();

      return CreatedAtAction("GetIdea", new { id = idea.IdeaId }, idea);
    }

    // DELETE: api/Idea/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Idea>> DeleteIdea(int id)
    {
      var idea = await _context.Ideas.FindAsync(id);
      if (idea == null)
      {
        return NotFound();
      }

      _context.Ideas.Remove(idea);
      await _context.SaveChangesAsync();

      return idea;
    }

    private bool IdeaExists(int id)
    {
      return _context.Ideas.Any(e => e.IdeaId == id);
    }
  }
}
